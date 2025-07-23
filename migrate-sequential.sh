#!/bin/bash

echo "API'yi başlatılıyor..."
cd src/API && dotnet run &
API_PID=$!

echo "API'nin başlamasını bekleniyor..."
sleep 15

echo "Tabloları silen migration çalıştırılıyor..."
curl -X POST http://localhost:5299/api/migrate

echo "Biraz bekleniyor..."
sleep 5

echo "Tüm migration'lar çalıştırılıyor..."
curl -X POST http://localhost:5299/api/migrate

echo "Biraz bekleniyor..."
sleep 5

echo "Seed data çalıştırılıyor..."
curl -X POST http://localhost:5299/api/seed

echo "Migration tamamlandı!" 