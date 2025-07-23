#!/bin/bash

echo "API'yi başlatılıyor..."
cd src/API && dotnet run &
API_PID=$!

echo "API'nin başlamasını bekleniyor..."
sleep 15

echo "Tabloları silen migration çalıştırılıyor..."
curl -X POST http://localhost:5299/api/migrate

echo "API durduruluyor..."
kill $API_PID

echo "Tablolar silindi!" 