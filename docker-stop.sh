#!/bin/bash

echo "🛑 MinimalAirbnb Docker Durdurma Script'i"
echo "========================================="

# Servisleri durdur
echo "🛑 Servisler durduruluyor..."
docker compose down

echo ""
echo "✅ MinimalAirbnb Docker ortamı durduruldu!"
echo ""
echo "📊 Container durumu:"
docker compose ps 