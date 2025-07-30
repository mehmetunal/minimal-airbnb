#!/bin/bash

echo "📋 MinimalAirbnb Docker Log Script'i"
echo "===================================="

# Hangi servisin loglarını görmek istediğini sor
echo "Hangi servisin loglarını görmek istiyorsunuz?"
echo "1) API"
echo "2) MSSQL"
echo "3) Tümü"
echo "4) Çıkış"

read -p "Seçiminizi yapın (1-4): " choice

case $choice in
    1)
        echo "📋 API Logları:"
        docker compose logs -f api
        ;;
    2)
        echo "📋 MSSQL Logları:"
        docker compose logs -f mssql
        ;;
    3)
        echo "📋 Tüm Loglar:"
        docker compose logs -f
        ;;
    4)
        echo "👋 Çıkış yapılıyor..."
        exit 0
        ;;
    *)
        echo "❌ Geçersiz seçim!"
        exit 1
        ;;
esac 