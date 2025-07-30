#!/bin/bash

echo "🚀 MinimalAirbnb Docker Başlatma Script'i (API + MSSQL)"
echo "======================================================"

# Mevcut container'ları durdur ve sil
echo "🧹 Mevcut container'lar temizleniyor..."
docker compose down -v

# Image'ları yeniden build et
echo "🔨 Docker image'ları build ediliyor..."
docker compose build --no-cache

# Servisleri başlat
echo "🚀 Servisler başlatılıyor..."
docker compose up -d

# Servislerin hazır olmasını bekle
echo "⏳ Servislerin hazır olması bekleniyor..."
sleep 30

# Database'i oluştur
echo "🗄️ Database oluşturuluyor..."
docker compose exec mssql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Super123! -C -Q "CREATE DATABASE MinimalAirbnb"

echo ""
echo "✅ MinimalAirbnb Docker ortamı başarıyla başlatıldı!"
echo ""
echo "📋 Servis URL'leri:"
echo "   🌐 API: http://localhost:7001"
echo "   🌐 API (HTTPS): https://localhost:7002"
echo "   🗄️ MSSQL: localhost:1433"
echo ""
echo "📊 Container durumu:"
docker compose ps 