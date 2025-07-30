#!/bin/bash

echo "🚀 MinimalAirbnb Database Migration Script"
echo "=========================================="

# Connection string
CONNECTION_STRING="Server=localhost,1433;Database=MinimalAirbnb;User Id=sa;Password=Super123!;TrustServerCertificate=true;MultipleActiveResultSets=true"

echo "📊 Veritabanı bağlantısı test ediliyor..."
docker exec -it minimalairbnb-mssql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Super123! -C -Q "SELECT name FROM sys.databases WHERE name = 'MinimalAirbnb'"

if [ $? -eq 0 ]; then
    echo "✅ Veritabanı bağlantısı başarılı"
else
    echo "❌ Veritabanı bağlantısı başarısız"
    exit 1
fi

echo ""
echo "🔄 Migration'lar çalıştırılıyor..."

# FluentMigrator ile migration'ları çalıştır
dotnet run --project src/Infrastructure -- --connection "$CONNECTION_STRING"

echo ""
echo "✅ Migration işlemi tamamlandı!"
echo ""
echo "📋 Veritabanı tabloları kontrol ediliyor..."
docker exec -it minimalairbnb-mssql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Super123! -C -d MinimalAirbnb -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME"

echo ""
echo "🎉 Database kurulumu tamamlandı!"
echo "📍 Connection String: $CONNECTION_STRING" 