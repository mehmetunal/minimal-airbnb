# Mini Airbnb - Modül Listesi ve Detayları (pwd.md)

## 1. Kullanıcı Yönetimi Modülü
**Amaç:** Kullanıcıların sisteme kayıt olması, giriş yapması ve rollerinin belirlenmesi.

### Alt Bileşenler:
- Kullanıcı kayıt (email, telefon, şifre, kullanıcı tipi)
- Giriş/çıkış işlemleri (JWT/token bazlı auth)
- Profil yönetimi (Ad, soyad, fotoğraf, telefon, adres vs.)
- Rol yönetimi (Misafir, Ev Sahibi, Admin)
- Şifre yenileme / onay emaili

---

## 2. İlan (Ev) Yönetimi Modülü
**Amaç:** Ev sahiplerinin ilan oluşturup evlerini sisteme eklemeleri.

### Alt Bileşenler:
- İlan oluşturma (başlık, açıklama, ev tipi, konum, kişi kapasitesi)
- Fotoğraf yükleme
- Oda detayları (oda, yatak, banyo sayısı, olanaklar: wifi, mutfak vb.)
- Lokasyon (Google Maps API ile)
- Takvim ile uygunluk yönetimi
- İlan düzenleme / silme işlemleri

---

## 3. Rezervasyon Modülü
**Amaç:** Misafirlerin ev kiralaması ve rezervasyon işlemleri.

### Alt Bileşenler:
- Tarih seçimi (takvim üzerinden)
- Kişi sayısı
- Fiyat hesaplama (günlük fiyat x gün sayısı + hizmet bedeli)
- Rezervasyon talebi gönderme
- Ev sahibinden onay (opsiyonel)
- Rezervasyon iptal/iade politikası
- Rezervasyon geçmişi listesi

---

## 4. Ödeme Modülü
**Amaç:** Güvenli ödeme işlemleri.

### Alt Bileşenler:
- Ödeme sağlayıcı entegrasyonu (Stripe, PayPal, iyzico)
- Fatura/sipariş oluşturma
- Güvenli ödeme (SSL, 3D Secure desteği)
- Admin için gelir/komisyon hesaplama
- İade işlemleri yönetimi

---

## 5. Yorum ve Puanlama Modülü
**Amaç:** Misafirlerin geri bildirim bırakması.

### Alt Bileşenler:
- Yorum ekleme (rezervasyon sonrası)
- Yıldızlı değerlendirme sistemi (1-5)
- Yorum düzenleme / silme
- Ortalama puan gösterimi
- Ev sahibi yanıtları

---

## 6. Arama ve Filtreleme Modülü
**Amaç:** Kullanıcıların ilanları kolayca bulabilmesi.

### Alt Bileşenler:
- Lokasyon bazlı arama (şehir, ülke, harita)
- Tarih filtresi (uygunluk takvimi)
- Fiyat aralığı filtresi
- Oda/kişi sayısı filtresi
- Puan sıralama ve filtreleme
- Özellik bazlı filtreleme (wifi, klima, mutfak vs.)

---

## 7. Mesajlaşma Modülü
**Amaç:** Misafir ve ev sahibi arasında iletişim sağlamak.

### Alt Bileşenler:
- İlan özelinde mesajlaşma ekranı
- Gerçek zamanlı mesaj (WebSocket veya polling)
- Bildirim sistemi (yeni mesaj bildirimi)
- Admin moderasyon desteği (opsiyonel)

---

## 8. Yönetim Paneli (Admin Modülü)
**Amaç:** Sistem genel yönetimi ve kontrol.

### Alt Bileşenler:
- Kullanıcı listesi ve yönetimi
- İlan onay/red, düzenleme, silme
- Rezervasyon takibi
- Raporlar ve analizler (toplam gelir, en çok kiralanan ilan)
- Komisyon oranı ayarlama
- Şikayet/ihlal takibi

---
## Public Kısmını unutmayalım oluşturuken Web Projesi -

## Ekstra Modüller (İsteğe Bağlı)

### ✅ Favoriler Modülü
- Misafirlerin favori ilanlarını kaydedip listeleyebilmesi

### ✅ Şikayet Bildirimi Modülü
- İlan ya da kullanıcı şikayet sistemi (spam, dolandırıcılık vs.)

### ✅ Çoklu Dil Desteği
- Uygulamanın çok dilli hale getirilmesi (TR, EN, DE vs.)

### ✅ Blog / Yardım Merkezi
- Kullanıcılara yönelik rehber içerikler, yardım makaleleri

---

## Teknoloji Önerileri

| Katman     | Öneri                          |
|------------|---------------------------------|
| Frontend   | Asp.net mvc                     |
| Backend    | .NET                            |
| Veritabanı | Mssql                           |
| Harita     | Google Maps API / OpenStreetMap |
| Auth       | JWT + OAuth2                    |
| Ödeme      | Entegrasyon yapılmıcaK          |
| Bildirim   | Email                           |

# Teknik Alt Yapıda Kullanılacak Middleware
- ApiResponseMiddleware
- ExceptionMiddleware
- IPFilterMiddleware



# Not
/media/mehmet/diskim/Project/maggsoft/src Burda benim yazdığım alt yapı framework var bunu kullanmamız gerekiyor o yüzden bu projeyi inceleyebilisin
- Örnek : dotnet add package Maggsoft.Core

## Maggsoft Framework ile Kullanılabilecek Altyapı ve Modüller

MinimalAirbnb projesinde Maggsoft altyapısından doğrudan veya genişletilerek kullanılabilecek başlıca modüller ve altyapı servisleri aşağıda özetlenmiştir:

- **Kullanıcı ve Rol Yönetimi**
  - Microsoft.AspNetCore.Identity ile tam uyumlu kullanıcı/rol yönetimi altyapısı
  - JWT tabanlı kimlik doğrulama ve yetkilendirme desteği
  - Şifre sıfırlama, email onayı, profil yönetimi için hazır servisler

- **Repository ve Veri Erişim Katmanı**
  - Generic Repository altyapısı (IRepository, IMssqlRepository)
  - MSSQL, PostgreSQL, MongoDB, Sqlite desteği (MinimalAirbnb için MSSQL önerilir)
  - Unit of Work ve transaction yönetimi

- **Servis Katmanı ve Otomatik Kayıt**
  - IService arayüzü ile servislerin otomatik DI kaydı
  - RegisterAll<IService>() ile servislerin kolay eklenmesi

- **AOP (Aspect Oriented Programming) ve Middleware**
  - TransactionScope, Exception, Validation gibi method interception desteği
  - Global Exception Middleware, ApiResponse Middleware, IPFilter Middleware hazır altyapısı

- **Cache (Önbellekleme)**
  - MemoryCache ve Redis ile dağıtık önbellek desteği
  - AddMaggsoftDistributedMemoryCache ile kolay kurulum

- **DTO, Mapper ve Validasyon**
  - DTO katmanı için temel arayüzler
  - AutoMapper ile nesne eşleme
  - FluentValidation ile model/doğrulama altyapısı

- **Logging ve İzleme**
  - Serilog ile gelişmiş loglama
  - Logların dosya, MSSQL veya Seq gibi servislere aktarımı

- **Seed Data ve Test Verisi**
  - Başlangıçta admin ve örnek kullanıcı/rol/ilan verisi ekleme desteği
  - Bogus ile sahte veri üretimi

- **Global Response Wrapper**
  - IResult ve Result sınıfları ile standart API dönüşleri
  - Başarılı/başarısız/validasyon mesajları için tek tip yanıt

- **Exception Handling**
  - Global Exception Handler ile tüm hataların standart formatta yakalanması

- **Dosya Yönetimi**
  - Fotoğraf/dosya yükleme ve yönetimi için FileManager servisleri

- **Gerçek Zamanlı Mesajlaşma ve Bildirim**
  - EventBus ve WebSocket/SignalR entegrasyonu için temel altyapı

- **Çoklu Dil ve Lokalizasyon**
  - Çoklu dil desteği için temel altyapı (isteğe bağlı)

- **Swagger ve API Dokümantasyonu**
  - Swagger/OpenAPI desteği ile otomatik API dokümantasyonu

- **Diğer Yardımcılar**
  - Gelişmiş Extension metodlar, Helper sınıflar, Guard, Singleton, IoC altyapısı

---

Bu altyapı sayesinde MinimalAirbnb projesinde; kullanıcı yönetimi, ilan yönetimi, rezervasyon, yorum, mesajlaşma, admin paneli gibi tüm ana modüller hızlı ve güvenli şekilde geliştirilebilir. Tüm altyapı .NET 8 ve Clean Architecture prensiplerine uygundur. Gerektiğinde Maggsoft kütüphaneleri doğrudan NuGet üzerinden eklenebilir veya kaynak koddan referans verilebilir.

## Afitap SSA Projesinden Alınacak Identity Yapısı ve Endpoint'ler

Afitap SSA projesindeki Microsoft.AspNetCore.Identity implementasyonu incelenerek, MinimalAirbnb projesi için kullanılabilecek yapı ve endpoint'ler aşağıda özetlenmiştir:

### Identity Yapılandırması ve Kurulum

**Program.cs'de Identity Konfigürasyonu:**
```csharp
// Authentication ve Authorization
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, opt =>
    {
        opt.ExpireTimeSpan = TimeSpan.FromDays(1);
    })
    .AddBearerToken(IdentityConstants.BearerScheme);

// Identity Core Konfigürasyonu
builder.Services.AddIdentityCore<AppIdentityUser>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = true;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    opt.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddRoles<AppIdentityRole>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<MinimalAirbnbDbContext>()
    .AddTokenProvider<CustomTokenProvider<AppIdentityUser>>(TokenOptions.DefaultProvider)
    .AddApiEndpoints();
```

### Kullanıcı Modeli (AppIdentityUser)

**MinimalAirbnb için Genişletilmiş Kullanıcı Sınıfı:**
```csharp
public class AppIdentityUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public UserType UserType { get; set; } // Guest, Host, Admin
    public bool IsVerified { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation Properties
    public ICollection<Property> Properties { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
```

### Rol Yapısı

**MinimalAirbnb için Rol Enum'u:**
```csharp
public enum UserType
{
    Guest,      // Misafir
    Host,       // Ev Sahibi  
    Admin       // Yönetici
}
```

### Identity Endpoint'leri

**MinimalAirbnb için IdentityApiEndpoint:**

1. **Kullanıcı Kayıt (Register)**
   - `POST /aut/register`
   - Email, şifre, ad, soyad, telefon, kullanıcı tipi
   - Email onayı gönderimi
   - Rol ataması (Guest/Host)

2. **Kullanıcı Giriş (Login)**
   - `POST /aut/login`
   - Email/şifre ile giriş
   - JWT token döndürme
   - IP ve User-Agent loglama

3. **Kullanıcı Çıkış (Logout)**
   - `POST /aut/logout`
   - Token geçersizleştirme
   - Logout event'i tetikleme

4. **Token Yenileme (Refresh)**
   - `POST /aut/refresh`
   - Refresh token ile yeni access token alma

5. **Email Onayı (Confirm Email)**
   - `GET /aut/confirmEmail`
   - Email onay linki ile hesap aktivasyonu

6. **Email Onayı Tekrar Gönderme**
   - `POST /aut/resendConfirmationEmail`
   - Onay emaili tekrar gönderimi

7. **Şifre Sıfırlama (Forgot Password)**
   - `POST /aut/forgotPassword`
   - Şifre sıfırlama emaili gönderimi

8. **Şifre Sıfırlama (Reset Password)**
   - `POST /aut/resetPassword`
   - Yeni şifre belirleme

9. **Kullanıcı Bilgileri (Info)**
   - `GET /aut/manage/info` (Authorized)
   - Kullanıcı profil bilgilerini getirme

10. **Kullanıcı Bilgileri Güncelleme**
    - `POST /aut/manage/info` (Authorized)
    - Profil bilgilerini güncelleme
    - Email değiştirme
    - Şifre değiştirme

11. **Claims Bilgileri**
    - `GET /aut/claims` (Authorized)
    - Kullanıcının tüm claim'lerini getirme

12. **İki Faktörlü Doğrulama (2FA)**
    - `POST /aut/manage/2fa` (Authorized)
    - 2FA etkinleştirme/devre dışı bırakma
    - Recovery codes yönetimi

### Özel Identity Servisleri

**CustomIdentityErrorDescriber:**
- Türkçe hata mesajları
- Şifre gereksinimleri
- Email validasyonu
- Kullanıcı adı kuralları

**CustomTokenProvider:**
- Özel token yaşam süresi (1 gün)
- Email onay token'ları
- Şifre sıfırlama token'ları

**CustomClaimsPrincipalFactory:**
- Özel claim'ler ekleme
- Kullanıcı tipi claim'i
- Profil bilgileri claim'leri

### Authorization Policies

**MinimalAirbnb için Authorization:**
```csharp
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("GuestOnly", policy => policy.RequireClaim("UserType", "Guest"))
    .AddPolicy("HostOnly", policy => policy.RequireClaim("UserType", "Host"))
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("UserType", "Admin"))
    .AddPolicy("VerifiedHost", policy => policy.RequireClaim("IsVerified", "true"))
    .AddPolicy("PropertyOwner", policy => policy.RequireClaim("PropertyOwner"));
```

### DTO Modelleri

**IdentityRegisterRequest:**
```csharp
public class IdentityRegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserType UserType { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
```

### Event Sistemi

**Login/Logout Event'leri:**
- Kullanıcı giriş logları
- IP adresi kaydetme
- User-Agent bilgisi
- Başarısız giriş denemeleri

### Güvenlik Özellikleri

- **Account Lockout:** 5 başarısız deneme sonrası 15 dakika kilitleme
- **Email Onayı:** Hesap aktivasyonu için zorunlu
- **Şifre Politikası:** Güçlü şifre gereksinimleri
- **JWT Token:** Bearer token ile API güvenliği
- **Refresh Token:** Token yenileme mekanizması

Bu yapı sayesinde MinimalAirbnb projesinde güvenli, ölçeklenebilir ve kullanıcı dostu bir kimlik doğrulama sistemi kurulabilir. Tüm endpoint'ler RESTful standartlara uygun ve Swagger dokümantasyonu ile desteklenmektedir. Burası sadece bir örnektir hepsini bizim MinimalAirbnb projesine uygulucak değiliz sadece burdan alabileceğimiz örneklerimiz var

---

## MinimalAirbnb Projesi - Mevcut Durum ve Yapılan İşlemler

### ✅ Tamamlanan İşlemler

#### 1. Proje Yapısı Oluşturuldu
- **Clean Architecture** prensiplerine uygun katmanlı yapı
- **Solution:** MinimalAirbnb.sln
- **Projeler:**
  - `MinimalAirbnb.Domain` - Domain katmanı (Entity'ler, Enum'lar, Interface'ler)
  - `MinimalAirbnb.Application` - Application katmanı (CQRS, Validators, DTOs)
  - `MinimalAirbnb.Infrastructure` - Infrastructure katmanı (DbContext, Repository, External Services)
  - `MinimalAirbnb.API` - API katmanı (Controllers, Middleware)
  - `MinimalAirbnb.Admin` - Admin Panel (MVC)
  - `MinimalAirbnb.Web` - Public Web Sitesi (MVC)

#### 2. NuGet Paket Yönetimi
- **Central Package Management** aktif
- **Directory.Packages.props** ile merkezi versiyon yönetimi
- **nuget.config** ile GitHub Packages entegrasyonu
- **GitHub Token** ile authentication sağlandı

#### 3. Maggsoft Framework Entegrasyonu
- **Maggsoft.Core** (v2.0.39) - Tüm projelere eklendi
- **Maggsoft.Mssql** (v2.0.12) - Infrastructure projesine eklendi
- **Entity Framework Core** v9.0.0'a güncellendi
- **NuGet paketleri** başarıyla yüklendi

#### 4. Temel Paketler Eklendi
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** (v8.0.5)
- **AutoMapper** (v13.0.1)
- **FluentValidation.AspNetCore** (v11.3.0)
- **MediatR** (v12.2.0)
- **Serilog.AspNetCore** (v8.0.1)
- **Swashbuckle.AspNetCore** (v7.1.0)
- **FluentMigrator** (v6.2.0)
- **Bogus** (v35.4.0)
- **AspectCore** (v2.4.0)

#### 5. Build Durumu
- ✅ **Build başarılı** - Tüm projeler derlendi
- ⚠️ **Uyarılar:** Package source mapping uyarıları (kritik değil)
- ⚠️ **Uyarılar:** Eski Libraries referansları (temizlendi)

### 📁 Proje Yapısı

```
MinimalAirbnb/
├── MinimalAirbnb.sln
├── Directory.Packages.props
├── nuget.config
├── pwd.md
└── src/
    ├── Domain/
    │   └── MinimalAirbnb.Domain.csproj
    ├── Application/
    │   └── MinimalAirbnb.Application.csproj
    ├── Infrastructure/
    │   └── MinimalAirbnb.Infrastructure.csproj
    ├── API/
    │   └── MinimalAirbnb.API.csproj
    ├── Admin/
    │   └── MinimalAirbnb.Admin.csproj
    └── Web/
        └── MinimalAirbnb.Web.csproj
```

### 🔧 Teknik Altyapı

#### Maggsoft Framework Modülleri
- ✅ **Core** - Temel altyapı, extension'lar, helper'lar
- ✅ **Mssql** - MSSQL repository ve data access
- 🔄 **Services** - Servis katmanı (NuGet'ten eklenecek)
- 🔄 **Aspect** - AOP ve method interception
- 🔄 **Cache** - Önbellekleme altyapısı
- 🔄 **Logging** - Gelişmiş loglama
- 🔄 **Endpoints** - API endpoint altyapısı

#### Identity ve Authentication
- ✅ **Microsoft.AspNetCore.Identity** hazır
- ✅ **JWT Bearer Token** desteği
- ✅ **Custom User Model** tasarımı hazır
- ✅ **Role-based Authorization** yapısı

### 🎯 Sonraki Adımlar

#### 1. Domain Katmanı
- [ ] Entity'ler oluşturulacak (User, Property, Reservation, Review, etc.)
- [ ] Enum'lar tanımlanacak (UserType, PropertyType, ReservationStatus, etc.)
- [ ] Interface'ler tanımlanacak (IRepository, IService, etc.)

#### 2. Application Katmanı
- [ ] CQRS Commands ve Queries oluşturulacak
- [ ] DTO'lar tanımlanacak
- [ ] Validator'lar yazılacak
- [ ] AutoMapper profilleri oluşturulacak

#### 3. Infrastructure Katmanı
- [ ] DbContext oluşturulacak
- [ ] Repository implementasyonları yazılacak
- [ ] Identity konfigürasyonu yapılacak
- [ ] Migration'lar oluşturulacak

#### 4. API Katmanı
- [ ] Controller'lar oluşturulacak
- [ ] Middleware'ler eklenecak
- [ ] Swagger konfigürasyonu yapılacak
- [ ] Identity endpoint'leri implement edilecek

#### 5. Admin ve Web Projeleri
- [ ] Layout'lar oluşturulacak
- [ ] Controller'lar yazılacak
- [ ] View'lar tasarlanacak
- [ ] Bootstrap 5 entegrasyonu yapılacak

### 🚀 Proje Hazır Durumda

MinimalAirbnb projesi artık **Maggsoft framework** ile entegre edilmiş, **Clean Architecture** prensiplerine uygun, **NuGet paketleri** yüklenmiş ve **build edilebilir** durumda. 

**Bir sonraki adım:** Domain katmanından başlayarak entity'leri ve temel yapıları oluşturmaya başlayabiliriz.
/media/mehmet/diskim/Project/maggsoft/src Burda benim yazdığım alt yapı framework var bunu kullanmamız gerekiyor o yüzden bu projeyi 
inceleyebilirmisin

# Maggsoft Nuget Paketleri
  - dotnet add package Maggsoft.Core --version 2.0.39
  - dotnet add package Maggsoft.Data --version 2.0.20
  - dotnet add package Maggsoft.Framework --version 2.3.6
  - dotnet add package Maggsoft.Data.Mssql --version 2.0.10
  - dotnet add package Maggsoft.Mssql --version 2.0.12
  - dotnet add package Maggsoft.Cache --version 2.0.22
  - dotnet add package Maggsoft.Cache.MemoryCache --version 2.0.7
  - dotnet add package Maggsoft.Services --version 2.0.8
  - dotnet add package Maggsoft.Aspect.Core --version 1.0.15
  - dotnet add package Maggsoft.Mssql.Services --version 2.0.17
  - dotnet add package Maggsoft.Endpoints --version 2.0.7
  - dotnet add package Maggsoft.Sqlite --version 2.0.8
  - dotnet add package Maggsoft.Data.Sqlite --version 2.0.12
  - dotnet add package Maggsoft.Sqlite.Services --version 2.0.10
  - dotnet add package Maggsoft.Ocelot.Core --version 1.0.6
  - dotnet add package Maggsoft.Data.Npgsql --version 1.0.11
  - dotnet add package Maggsoft.Cache.Redis --version 2.0.3
  - dotnet add package Maggsoft.Data.Mongo --version 1.0.3
  - dotnet add package Maggsoft.EventBus.IoC --version 1.0.16
  - dotnet add package Maggsoft.EventBus.RabbitMQ --version 1.0.17
  - dotnet add package Maggsoft.EventBus.AzureServiceBus --version 1.0.15
  - dotnet add package Maggsoft.Dto.Sqlite --version 2.0.2
  - dotnet add package Maggsoft.Dto.Mongo --version 1.0.2
  - dotnet add package Maggsoft.Dto.Npgsql --version 1.0.2,
  - dotnet add package Maggsoft.Mongo.Services --version 1.0.8
  - dotnet add package Maggsoft.Dto.Mssql --version 1.0.2
  - dotnet add package Maggsoft.Npgsql.Services --version 1.0.8
  - dotnet add package Maggsoft.EventBus --version 1.0.19

---

## 🔍 Maggsoft Framework Detaylı Analizi - MinimalAirbnb için Kullanılabilir Paketler

### 📦 **Maggsoft.Core** (v2.0.39) - ✅ Zaten Ekli
**Domain ve Application katmanları için temel altyapı**

#### Kullanılabilir Bileşenler:
- **Base Classes:**
  - `IResult<T>` / `Result<T>` - Standart API response wrapper
  - `IEntity` - Entity base interface
  - `Guard` - Null/validation guard'ları
  - `Singleton<T>` / `BaseSingleton<T>` - Singleton pattern

- **Repository Pattern:**
  - `IRepository<T>` - Generic repository interface (CRUD operations)
  - Async/await desteği
  - Expression-based filtering
  - Bulk operations (AddRange, UpdateRange, DeleteRange)

- **Extensions:**
  - `StringExtensions` - 27KB'lık kapsamlı string helper'ları
  - `DateTimeExtensions` - Tarih işlemleri
  - `EnumerableExtensions` - Collection helper'ları
  - `PaginationExtension` - Sayfalama desteği
  - `LinqSelectExtensions` - LINQ optimizasyonları

- **Infrastructure:**
  - `MaggsoftContext` - Context management
  - `MaggsoftEngine` - Engine pattern
  - `HashHelper` - Hash işlemleri

### 📦 **Maggsoft.Services** (v2.0.8) - 🔄 Eklenebilir
**Application katmanı için servis altyapısı**

#### Kullanılabilir Bileşenler:
- **Extensions:**
  - `MappingExtensions` - AutoMapper helper'ları
  - `IncludableExtensions` - Entity Framework Include optimizasyonları

- **Security:**
  - Güvenlik servisleri

- **RazorEngine:**
  - Template engine desteği

- **Events:**
  - Event handling altyapısı

### 📦 **Maggsoft.Mssql.Services** (v2.0.17) - 🔄 Eklenebilir
**Infrastructure katmanı için MSSQL servisleri**

#### Kullanılabilir Bileşenler:
- **Base Services:**
  - `BaseService` - Temel servis sınıfı
  - `MssqlBaseServices` - MSSQL-specific servisler

- **Messages:**
  - Mesajlaşma altyapısı

### 📦 **Maggsoft.Data** (v2.0.20) - 🔄 Eklenebilir
**Domain katmanı için data altyapısı**

#### Kullanılabilir Bileşenler:
- **Base Entities:**
  - `IBaseEntity` - Gelişmiş entity interface
  - `BaseEntity` - Base entity sınıfı
  - `ILocalizedEntity` - Çoklu dil desteği
  - `ILanguageModel` - Dil modeli

- **Migration:**
  - FluentMigrator entegrasyonu

- **Extensions:**
  - Data-specific extension'lar

### 📦 **Maggsoft.Cache** (v2.0.22) - 🔄 Eklenebilir
**Infrastructure katmanı için cache altyapısı**

#### Kullanılabilir Bileşenler:
- **Cache Interface:**
  - `ICache` - Cache interface'i

- **Attributes:**
  - Cache attribute'ları

- **Helpers:**
  - Cache helper'ları

- **Extensions:**
  - `ServiceCollectionExtension` - DI registration

### 📦 **Maggsoft.Cache.MemoryCache** (v2.0.7) - 🔄 Eklenebilir
**Memory cache implementasyonu**

### 📦 **Maggsoft.Cache.Redis** (v2.0.3) - 🔄 Eklenebilir
**Redis cache implementasyonu**

### 📦 **Maggsoft.Aspect.Core** (v1.0.15) - 🔄 Eklenebilir
**AOP (Aspect Oriented Programming) altyapısı**

#### Kullanılabilir Bileşenler:
- **Aspects:**
  - `IAspect` / `IAspectAsync` - Aspect interface'leri
  - `AspectAttribute` - Aspect attribute'ları

- **Decorators:**
  - Method decorator'ları

- **DispatchProxy:**
  - Proxy pattern implementasyonu

- **Extensions:**
  - `ServiceCollectionExtension` - DI registration

### 📦 **Maggsoft.Endpoints** (v2.0.7) - 🔄 Eklenebilir
**API katmanı için endpoint altyapısı**

#### Kullanılabilir Bileşenler:
- **Extensions:**
  - `EndpointExtensions` - Endpoint helper'ları

- **Abstractions:**
  - Endpoint abstraction'ları

### 📦 **Maggsoft.Logging** (v1.0.6) - 🔄 Eklenebilir
**Logging altyapısı**

#### Kullanılabilir Bileşenler:
- **Logging Factory:**
  - `MaggsoftSerilogLoggerFactory` - Serilog factory

- **Enrichers:**
  - `ThreadIdEnricher` - Thread ID enrichment

- **Aspect:**
  - Logging aspect'leri

### 📦 **Maggsoft.EventBus** (v1.0.19) - 🔄 Eklenebilir
**Event bus altyapısı**

### 📦 **Maggsoft.EventBus.RabbitMQ** (v1.0.17) - 🔄 Eklenebilir
**RabbitMQ event bus**

### 📦 **Maggsoft.EventBus.AzureServiceBus** (v1.0.15) - 🔄 Eklenebilir
**Azure Service Bus event bus**

### 📦 **Maggsoft.Dto.Mssql** (v1.0.2) - 🔄 Eklenebilir
**MSSQL DTO'ları**

---

## 🎯 MinimalAirbnb için Önerilen Paket Ekleme Sırası

### 1. **Application Katmanı için:**
```bash
dotnet add src/Application/MinimalAirbnb.Application.csproj package Maggsoft.Services
dotnet add src/Application/MinimalAirbnb.Application.csproj package Maggsoft.Data
```

### 2. **Infrastructure Katmanı için:**
```bash
dotnet add src/Infrastructure/MinimalAirbnb.Infrastructure.csproj package Maggsoft.Mssql.Services
dotnet add src/Infrastructure/MinimalAirbnb.Infrastructure.csproj package Maggsoft.Cache
dotnet add src/Infrastructure/MinimalAirbnb.Infrastructure.csproj package Maggsoft.Cache.MemoryCache
dotnet add src/Infrastructure/MinimalAirbnb.Infrastructure.csproj package Maggsoft.Aspect.Core
dotnet add src/Infrastructure/MinimalAirbnb.Infrastructure.csproj package Maggsoft.Logging
```

### 3. **API Katmanı için:**
```bash
dotnet add src/API/MinimalAirbnb.API.csproj package Maggsoft.Endpoints
dotnet add src/API/MinimalAirbnb.API.csproj package Maggsoft.EventBus
```

### 4. **Domain Katmanı için:**
```bash
dotnet add src/Domain/MinimalAirbnb.Domain.csproj package Maggsoft.Data
```

---

## 🔧 Kullanım Senaryoları

### **Domain Katmanı:**
- `IEntity` - Tüm entity'ler için base interface
- `IBaseEntity` - Gelişmiş entity özellikleri
- `ILocalizedEntity` - Çoklu dil desteği

### **Application Katmanı:**
- `IRepository<T>` - Repository pattern
- `Result<T>` - Standart API responses
- `MappingExtensions` - AutoMapper helper'ları

### **Infrastructure Katmanı:**
- `Repository<T>` - MSSQL repository implementasyonu
- `UnitOfWork` - Transaction management
- `ICache` - Cache altyapısı
- `AspectAttribute` - Method interception

### **API Katmanı:**
- `EndpointExtensions` - Endpoint helper'ları
- `EventBus` - Event-driven architecture

---

## 📋 Sonraki Adımlar

1. **Önerilen paketleri ekle**
2. **Domain entity'lerini oluştur** (IEntity'den inherit)
3. **Repository interface'lerini tanımla** (IRepository<T>)
4. **Application servislerini yaz** (Result<T> kullan)
5. **Infrastructure implementasyonlarını yap** (Repository<T>, UnitOfWork)
6. **API controller'larını oluştur** (EndpointExtensions kullan)

---

## 🏗️ **Maggsoft.Framework** (v2.2.34) - 🔄 Eklenebilir
**API ve Presentation katmanları için kapsamlı framework altyapısı**

### 📦 **Proje Konumu:**
`../maggsoft/src/Presentation/Maggsoft.Framework/`

### 🎯 **Kullanım Amacı:**
Maggsoft.Framework, MinimalAirbnb projesinin **API katmanı** için kapsamlı bir framework altyapısı sağlar. Tüm middleware'ler, authentication, authorization, exception handling, API response formatting ve diğer presentation katmanı ihtiyaçlarını karşılar.

### 🔧 **Detaylı Klasör Analizi:**

#### 1. **📁 Exceptions** - Özel Exception Sınıfları
- **`ModelStateException`** (893B) - Validation exception'ları
  - ModelState error'larını JSON formatında serialize eder
  - String array, IEnumerable, List<string> desteği
  - Serialization desteği

- **`ApiVersioningException`** (560B) - API version exception'ları
  - API version uyumsuzluğu durumlarında kullanılır
  - Serialization desteği

- **`ForbiddenExtension`** (539B) - 403 Forbidden exception
  - Yetkisiz erişim durumlarında kullanılır
  - Serialization desteği

#### 2. **📁 Api** - API Base Sınıfları
- **`BaseApiController`** (225B) - Tüm controller'lar için base class
  - `[Produces("application/json")]` attribute'u
  - `[ApiController]` attribute'u
  - MinimalAirbnb controller'ları bu sınıftan inherit edecek

#### 3. **📁 Extensions** - Extension Metodları
- **`ModelStateExtensions`** (385B) - ModelState helper'ları
  - `GetErrorMessages()` - ModelState error'larını string listesine çevirir

- **`HttpRequestExtensions`** (3.1KB) - HTTP request helper'ları
  - `IsMobileDevice()` - Mobil cihaz tespiti (regex ile)
  - `IsLocal()` - Local request tespiti

- **`SwaggerGenExtensions`** (3.7KB) - Swagger konfigürasyonu
  - OAuth2 entegrasyonu
  - XML documentation desteği
  - Custom operation filters

- **`JwtlExtensions`** (9.6KB) - JWT konfigürasyonu
  - JWT Bearer token ayarları
  - Identity Server entegrasyonu
  - Token validation events

- **`ApiVersioningExtensions`** (2.0KB) - API versioning
- **`CrosExtensions`** (1.1KB) - CORS ayarları
- **`IPFilterExtensions`** (1.0KB) - IP filtering
- **`CustomMiddlewareWithOptionsExtensions`** (509B) - Middleware options
- **`ApplicationBuilderExtensions`** (611B) - Application builder
- **`StartupConfigExtensions`** (2.4KB) - Startup konfigürasyonu

#### 4. **📁 Helper** - Yardımcı Sınıflar
- **📁 ModelStateResponseFactory**
  - **`ModelStateFeatureFilter`** (1.0KB) - ModelState error handling
    - Validation error'larını ModelStateException olarak fırlatır
    - JSON formatında error mesajları

- **📁 Configuration**
  - **`ShellHelpers`** (881B) - Shell komut çalıştırma
    - Bash komutları çalıştırma desteği
    - Linux sistemler için

- **📁 ApiVersioning**
  - **`ApiVersioningErrorResponseProvider`** (401B) - API version error handling
    - API version uyumsuzluğunda ApiVersioningException fırlatır

- **📁 Excel**
  - **`ExcelOperations`** (4.3KB) - Excel import/export
    - Generic Excel export fonksiyonu
    - OpenXML kullanarak Excel dosyası oluşturma
    - DataTable'dan Excel'e dönüştürme
    - Styling desteği (header bold, borders)

#### 5. **📁 JsonConverters** - JSON Dönüştürücüler
- **`TimeSpanConverter`** (527B) - TimeSpan JSON dönüştürme
  - TimeSpan'ı string olarak serialize/deserialize eder

- **`DecimalToStringConverter`** (1.3KB) - Decimal JSON dönüştürme
  - Decimal'ı string olarak serialize/deserialize eder
  - Türkçe kültür desteği (tr-TR)
  - Binlik ayırıcı desteği

#### 6. **📁 Systems** - Sistem Konfigürasyonu
- **`ServiceCollectionExtension`** (4.4KB) - DI ve Middleware Registration
  - **`AddInfrastructure()`** - Tüm servisleri tek seferde ekler:
    - JWT Authentication
    - Swagger/OpenAPI
    - CORS
    - API Versioning
    - Response Compression (Gzip, Brotli)
    - Exception Handling
    - ModelState Validation
    - IP Filtering
    - Logging

  - **`AddInfrastructure()`** - WebApplication konfigürasyonu:
    - Development/Production ortam ayarları
    - Static files
    - Swagger UI
    - Routing
    - CORS
    - Middleware pipeline
    - Authentication/Authorization
    - Status code pages

#### 7. **📁 Middleware** - Middleware Sınıfları
- **`ExceptionMiddleware`** (14KB) - Global exception handling
- **`ApiResponseMiddleware`** (5.1KB) - Global API response formatting
- **`IPFilterMiddleware`** (4.5KB) - IP filtering

#### 8. **📁 Security** - Güvenlik
- **📁 Authorization**
  - **`AuthorizeCheckOperationFilter`** - Swagger authorization filter
- **📁 Token**
  - **`AccessTokenHandler`** - Token yönetimi
  - **`IAccessTokenHandler`** - Token handler interface
- **📁 Model**
  - **`ApiTokenOptions`** - JWT token ayarları

#### 9. **📁 HttpClientApi** - HTTP Client
- **`MaggsoftHttpClient`** (9.6KB) - Gelişmiş HTTP client
- **`IMaggsoftHttpClient`** - HTTP client interface

#### 10. **📁 Options** - Konfigürasyon Seçenekleri
- **`CustomExceptionHandlerOptions`** - Exception handler ayarları
- **`IPFilterOptions`** - IP filtering ayarları

### 🎯 **MinimalAirbnb için Kullanım Senaryoları:**

#### **API Katmanı (MinimalAirbnb.API):**
```csharp
// Program.cs'de
services.AddInfrastructure(configuration);
app.AddInfrastructure();

// Bu tek satır ile şunlar eklenir:
// - Global Exception Handling (ModelStateException, ApiVersioningException, ForbiddenExtension)
// - API Response Formatting (Result<T> formatında)
// - JWT Authentication (Identity Server entegrasyonu)
// - Swagger Documentation (OAuth2 desteği ile)
// - CORS Configuration
// - Response Compression (Gzip, Brotli)
// - IP Filtering (Güvenlik)
// - API Versioning (Versiyonlama)
// - ModelState Validation (Otomatik validation error handling)
// - Excel Export (Admin paneli için)
// - Mobile Device Detection (Responsive design için)
```

#### **Controller'larda:**
```csharp
public class PropertyController : BaseApiController
{
    // Tüm controller'lar için base class
    // Standart response format
    // Exception handling
    // Authentication/Authorization
    // Swagger documentation otomatik
}

// Excel export örneği
public IActionResult ExportProperties()
{
    var properties = _propertyService.GetAll();
    var excelData = ExcelOperations.ExportToExcel(properties, "Properties");
    return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "properties.xlsx");
}
```

#### **Exception Handling:**
```csharp
// ModelState validation error'ları otomatik yakalanır
// ModelStateException olarak fırlatılır ve standart formatta döner

// API version uyumsuzluğu
// ApiVersioningException olarak fırlatılır

// Yetkisiz erişim
// ForbiddenExtension olarak fırlatılır
```

#### **JSON Serialization:**
```csharp
// Decimal'lar string olarak serialize edilir (Türkçe format)
// TimeSpan'lar string olarak serialize edilir
// ModelState error'ları JSON formatında döner
```

### 📋 **Ekleme Komutu:**
```bash
dotnet add src/API/MinimalAirbnb.API.csproj package Maggsoft.Framework
```

### 🔄 **Bağımlılıklar:**
- **Maggsoft.Core** (zaten ekli)
- **Maggsoft.Data** (eklenecek)
- **Microsoft.AspNetCore.Authentication.JwtBearer**
- **Swashbuckle.AspNetCore**
- **Microsoft.AspNetCore.Mvc.Versioning**
- **Azure.Extensions.AspNetCore.DataProtection.Keys**
- **DocumentFormat.OpenXml** (Excel işlemleri için)
- **FastMember** (Excel export için)

### 💡 **Avantajları:**
1. **Hazır Middleware'ler** - Exception handling, response formatting, IP filtering
2. **JWT Authentication** - Tam konfigürasyon ile Identity Server entegrasyonu
3. **API Versioning** - Versiyonlama desteği
4. **Swagger Integration** - OAuth2 desteği ile otomatik API dokümantasyonu
5. **Response Compression** - Gzip ve Brotli ile performans optimizasyonu
6. **Security Features** - IP filtering, authorization, mobile device detection
7. **Excel Operations** - Generic Excel export/import desteği
8. **HTTP Client** - Gelişmiş HTTP istekleri
9. **ModelState Validation** - Otomatik validation error handling
10. **JSON Converters** - Decimal ve TimeSpan için özel dönüştürücüler
11. **Shell Operations** - Linux sistemlerde bash komut çalıştırma

### 🎯 **Önerilen Kullanım:**
Maggsoft.Framework, MinimalAirbnb API projesinin **temel altyapısını** oluşturacak. Tüm middleware'ler, authentication, authorization, API infrastructure, Excel operations ve diğer presentation katmanı ihtiyaçlarını tek paket ile sağlar. **API katmanına mutlaka eklenmelidir.**

### 🔧 **Özel Özellikler:**
- **Mobile Device Detection** - Responsive design için mobil cihaz tespiti
- **Excel Export** - Admin paneli için rapor export
- **Turkish Culture Support** - Decimal formatlamada Türkçe desteği
- **Shell Operations** - Linux sistemlerde bash komut çalıştırma
- **OAuth2 Swagger Integration** - Swagger'da OAuth2 authentication
- **Response Compression** - Gzip ve Brotli ile performans
- **IP Filtering** - Güvenlik için IP bazlı erişim kontrolü

---

## 📋 **AFİTAP SSA PROJESİ DETAYLI ANALİZİ**

### 🎯 **Proje Genel Bakış:**
Afitap SSA projesi, **Spor Salonu Yönetim Sistemi** olarak tasarlanmış kapsamlı bir .NET 8 projesidir. Clean Architecture prensiplerine uygun olarak geliştirilmiş ve Maggsoft framework'ü kullanılarak oluşturulmuştur.

### 🏗️ **Proje Yapısı:**

#### **📁 Presentation Katmanı:**
- **`SSA.Api`** - Ana API projesi (253 satır Program.cs)
- **`SSA.Web`** - MVC Web projesi (110 satır Program.cs)
- **`SSA.Web.Framework`** - Web framework altyapısı
- **`SSA.BackgroundServices.API`** - Background service API'si

#### **📁 Libraries Katmanı:**
- **`SSA.Data.Mssql`** - Entity'ler ve DbContext
- **`SSA.Dto.Mssql`** - DTO'lar (Add, Edit, Result)
- **`SSA.Mssql.Services`** - Business logic katmanı
- **`SSA.Endpoints.Api`** - Minimal API endpoints
- **`SSA.IdentityManager`** - Identity yönetimi
- **`SSA.BackgroundServices`** - Background service'ler
- **`SSA.SmsProviders`** - SMS provider'ları

### 🔧 **Teknik Özellikler:**

#### **1. API Katmanı (SSA.Api) - Program.cs Analizi:**

```csharp
// JSON Konfigürasyonu
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.Converters.Add(new DecimalToStringConverter());
    options.SerializerOptions.Converters.Add(new TimeSpanConverter());
});

// MSSQL Konfigürasyonu
builder.Services
    .AddMssqlConfig<SSADbContext>(builder.Configuration, o =>
    {
        o.UseCompatibilityLevel(120); // Hosting uyumluluğu
    })
    .AddFluentMigratorConfig(builder.Configuration);

// AutoMapper Konfigürasyonu
builder.Services.AddAutoMapperConfig(p => p.AddProfile<AutoMapping>(), typeof(Program));

// Authorization Policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ViewProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.view"))
    .AddPolicy("CreateProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.create"))
    .AddPolicy("UpdateProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.update"))
    .AddPolicy("AdminAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.admin"))
    .AddPolicy("CoachAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.coach"))
    .AddPolicy("StudentAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.student"));

// Identity Konfigürasyonu
builder.Services.AddIdentityCore<AppIdentityUser>(opt =>
{
    opt.Password.RequiredLength = 1;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
    opt.Lockout.MaxFailedAccessAttempts = 5;
})
.AddRoles<AppIdentityRole>()
.AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
.AddErrorDescriber<CustomIdentityErrorDescriber>()
.AddEntityFrameworkStores<SSADbContext>()
.AddTokenProvider<CustomTokenProvider<AppIdentityUser>>(TokenOptions.DefaultProvider)
.AddApiEndpoints();

// Service Registration
builder.Services.AddSingleton<IEventPublisher, EventPublisher>();
builder.Services.AddScoped(typeof(IMssqlRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmailAccountService, EmailAccountService>();
builder.Services.AddScoped<ISmtpBuilder, SmtpBuilder>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.RegisterAll<IService>();
builder.Services.RegisterEventConsumer();

// Settings Registration (Dynamic)
var settings = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(ISettings).IsAssignableFrom(p) && p.IsClass == true);

foreach (var setting in settings)
{
    builder.Services.AddScoped(setting, serviceProvider =>
    {
        return serviceProvider
        .GetRequiredService<ISettingService>()
        .LoadAsync(setting).Result;
    });
}

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// File Management
builder.Services.AddSingleton<IFileManagerProviderBase>(
   new FileManagerProviderBase(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddTransient<IFilesManager, FilesManager>();
builder.Services.AddTransient<IMaggsoftFileProvider, MaggsoftFileProvider>();

// Form Options (File Upload)
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// Global Response Middleware
builder.Services.AddGlobalResponseMiddlewareWithOptions(p => p.IgnoreAcceptHeader = ["image/", "txt"]);
```

#### **2. Web Katmanı (SSA.Web) - Program.cs Analizi:**

```csharp
// JSON Konfigürasyonu
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        options.JsonSerializerOptions.Converters.Add(new DecimalToStringConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
    });

// HTTP Client Konfigürasyonu
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IMaggsoftHttpClient, CustomHttpClient>();

// Service Registration
builder.Services.AddScoped<IWebBaseService, WebBaseService>();
builder.Services.RegisterAll<IAutomaticRegisterWebServices>();
builder.Services.AddScoped<IBussensMessageService, BussensMessageService>();

// AutoMapper
builder.Services.AddAutoMapper(p => p.AddProfile<AutoMapping>(), typeof(Program));

// Session
builder.Services.AddSession();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Account/Login";
        opt.AccessDeniedPath = "/Account/AccessDenied";
        opt.ExpireTimeSpan = TimeSpan.FromDays(1);
        opt.Cookie.HttpOnly = true;
        opt.Cookie.IsEssential = true;
        opt.SlidingExpiration = true;
        opt.Events.OnSignedIn = context =>
        {
            var httpContext = context.HttpContext;
            httpContext.Items["Properties"] = context.Properties;
            httpContext.Features.Set(context.Properties);
            return Task.CompletedTask;
        };
    });

// Authorization Policies (API ile aynı)
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ViewProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.view"))
    .AddPolicy("CreateProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.create"))
    .AddPolicy("UpdateProjects", policy => policy.RequireClaim(CustomClaimTypes.Permission, "projects.update"))
    .AddPolicy("AdminAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.admin"))
    .AddPolicy("CoachAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.coach"))
    .AddPolicy("StudentAccount", policy => policy.RequireClaim(CustomClaimTypes.Permission, "account.student"));

// Form Options
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
```

### 📊 **Entity Yapısı:**

#### **1. AppIdentityUser (73 satır):**
```csharp
public class AppIdentityUser : IdentityUser<Guid>
{
    public Guid CompanyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PartnerFullName { get; set; }
    public string? PartnerPhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? CreatorUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? ModifierUserId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublish { get; set; }
    public string? Level { get; set; }
    public string? LevelDocumentPath { get; set; }
    public string? Avatar { get; set; }

    // Navigation Properties
    public Company Company { get; set; }
    public ICollection<AdvancePayment> AdvancePayments { get; set; }
    public ICollection<Due> Dues { get; set; }
    public ICollection<Expenses> Expenses { get; set; }
    public ICollection<Salaries> Salaries { get; set; }
    public ICollection<StudentAttendance> StudentAttendances { get; set; }
    public ICollection<TeamStudents> TeamStudents { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public void SoftDelete()
    {
        IsPublish = false;
        IsDeleted = true;
    }

    public static AppIdentityUser Create(...) { ... }
}
```

#### **2. Company (26 satır):**
```csharp
public sealed class Company : BaseEntity
{
    public string? CompanyName { get; set; }
    public string? Code { get; set; }
    public string? Logo { get; set; }
    public bool? IsPublicAdd { get; set; }

    // Navigation Properties
    public ICollection<AdvancePayment> AdvancePayments { get; set; }
    public ICollection<Due> Dues { get; set; }
    public ICollection<Expenses> Expenses { get; set; }
    public ICollection<TeamStudents> TeamStudents { get; set; }
    public ICollection<Team> Teams { get; set; }
    public ICollection<Match> Matches { get; set; }
    public ICollection<Salaries> Salaries { get; set; }
    public ICollection<StudentAttendance> StudentAttendances { get; set; }
    public ICollection<Workouts> Workouts { get; set; }
    public ICollection<AppIdentityUser> AspNetUsers { get; set; }
}
```

### 📋 **DTO Yapısı:**

#### **1. AppIdentityUserAddDto (25 satır):**
```csharp
public class AppIdentityUserAddDto : BaseDtoModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PartnerFullName { get; set; }
    public string? PartnerPhoneNumber { get; set; }
    public bool IsPublish { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public IdentityAppRole Role { get; set; } = IdentityAppRole.Admin;
    public Guid? CompanyId { get; set; } = null;
    public string? Level { get; set; }
    public IFormFile? LevelFile { get; set; }
}
```

#### **2. AppIdentityUserResultDto (40 satır):**
```csharp
public class AppIdentityUserResultDto : BaseDtoModel
{
    public Guid CompanyId { get; set; }
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PartnerFullName { get; set; }
    public string? PartnerPhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? CreatorUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? ModifierUserId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublish { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string? TeamName { get; set; }
    public string? Level { get; set; }
    public string? LevelDocumentPath { get; set; }
    public string? Avatar { get; set; }
}
```

### 🔧 **Service Katmanı:**

#### **1. IUserService Interface (26 satır):**
```csharp
public interface IUserService : IService
{
    Task<IList<AppIdentityUserResultDto>> GetUsersInRoleAsync(IdentityAppRole role);
    Task<IList<AppIdentityUserResultDto>> GetUsersInRoleNoCompanyAsync(IdentityAppRole role);
    Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>> GetUsersInRoleAsync(DataTablesRequest requestModel, IdentityAppRole? role = IdentityAppRole.Student);
    Task<AppIdentityUserResultDto> GetByIdAsync(Guid id);
    Task<Result<AppIdentityUserResultDto>> AddAsync(AppIdentityUserAddDto dto);
    Task<Result<AppIdentityUserResultDto>> UpdateAsync(AppIdentityUserEditDto dto);
    Task<AppIdentityUserResultDto> AvatarUploadAsync(AppIdentityUserAvatarDto dto);
    Task<AppIdentityUserResultDto> DeleteAsync(Guid id);
    Task<AppIdentityUserResultDto> RecoveryAsync(Guid id);
    Task<AppIdentityUserResultDto> PublisAsync(Guid id);
    Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>> GetCoachListByTeamIdAsync(DataTablesRequest requestModel, Guid teamId);
    Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>> GetNonAppointedCoachAsync(DataTablesRequest requestModel, Guid teamId);
    Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>> GetStudentListByTeamIdAsync(DataTablesRequest requestModel, Guid teamId);
    Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>> GetNonAppointedStudentAsync(DataTablesRequest requestModel, Guid teamId);
}
```

#### **2. UserService Implementation (680 satır):**
- **Caching** - Redis/Memory cache kullanımı
- **AutoMapper** - Entity-DTO dönüşümleri
- **Event Publishing** - Domain event'leri
- **File Management** - Avatar upload işlemleri
- **Encryption** - Veri şifreleme
- **Validation** - FluentValidation
- **DataTables** - Sayfalama ve filtreleme
- **Role-based Access** - Rol bazlı erişim kontrolü

### 🌐 **Endpoint Yapısı:**

#### **UserApiEndpoint (146 satır):**
```csharp
public sealed class UserApiEndpoint : IApiEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        var routeGroup = endpoints.MapGroup("user")
            .WithTags("User")
            .RequireAuthorization();

        // GET Endpoints
        routeGroup.MapGet("/list", [IgnoreResponseRewindMiddleware] async Task<DataTablesResponse<IEnumerable<AppIdentityUserResultDto>>>
            ([FromServices] IUserService userService,
            DataTablesRequest requestModel,
            IdentityAppRole role) =>
           await userService.GetUsersInRoleAsync(requestModel, role));

        routeGroup.MapGet("/listByRole", async Task<IEnumerable<AppIdentityUserResultDto>>
            ([FromServices] IUserService userService,
            IdentityAppRole role) =>
           await userService.GetUsersInRoleAsync(role));

        routeGroup.MapGet("/getbyid", async Task<AppIdentityUserResultDto>
            ([FromServices] IUserService userService,
            [FromQuery] Guid id) =>
           await userService.GetByIdAsync(id));

        // POST Endpoints
        routeGroup.MapPost("/add", async Task<Result<AppIdentityUserResultDto>>
            ([FromServices] IUserService userService,
            AppIdentityUserAddDto dto) =>
           await userService.AddAsync(dto));

        routeGroup.MapPost("/addcontent", async Task<Result<AppIdentityUserResultDto>>
            ([FromServices] IUserService userService,
            [FromForm] AppIdentityUserAddDto dto) =>
           await userService.AddAsync(dto))
            .DisableAntiforgery();

        routeGroup.MapPost("/publicadd", [AllowAnonymous] async Task<Result<AppIdentityUserResultDto>>
            ([FromServices] IUserService userService,
            AppIdentityUserAddDto dto) =>
           await userService.AddAsync(dto));

        routeGroup.MapPost("/avatarupload", async Task<AppIdentityUserResultDto>
            ([FromServices] IUserService userService,
            [FromForm] AppIdentityUserAvatarDto dto) =>
            {
                return await userService.AvatarUploadAsync(dto);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var contanctValidator = context.HttpContext
                    .RequestServices
                    .GetService<IValidator<AppIdentityUserAvatarDto>>();

                if (contanctValidator == null)
                    return Results.BadRequest();

                var dtoParam = context
                    .GetArgument<AppIdentityUserAvatarDto>(1);

                var validationResult = contanctValidator
                    .Validate(dtoParam);

                if (validationResult.IsValid)
                    return await next(context);

                var errorMessages = validationResult.Errors.Select(s => s.ErrorMessage);
                throw new ModelStateException(errorMessages);
            })
            .DisableAntiforgery();

        // CRUD Operations
        routeGroup.MapPost("/update", async Task<Result<AppIdentityUserResultDto>>
            ([FromServices] IUserService userService,
            AppIdentityUserEditDto dto) =>
           await userService.UpdateAsync(dto));

        routeGroup.MapPost("/delete", async Task<AppIdentityUserResultDto>
            ([FromServices] IUserService userService,
            [FromQuery] Guid id) =>
           await userService.DeleteAsync(id));

        routeGroup.MapPost("/recovery", async Task<AppIdentityUserResultDto>
            ([FromServices] IUserService userService,
            [FromQuery] Guid id) =>
           await userService.RecoveryAsync(id));

        routeGroup.MapPost("/publish", async Task<AppIdentityUserResultDto>
            ([FromServices] IUserService userService,
            [FromQuery] Guid id) =>
           await userService.PublisAsync(id));
    }
}
```

### 🎨 **Web Framework:**

#### **1. CustomHttpClient (293 satır):**
- **Token Management** - JWT token yönetimi
- **Error Handling** - HTTP error handling
- **Authentication** - Cookie-based authentication
- **File Upload** - Multipart form data
- **Response Parsing** - JSON response parsing
- **Logging** - Error logging

#### **2. BaseController (148 satır):**
- **HTTP Client** - API iletişimi
- **Message Service** - Kullanıcı mesajları
- **AutoMapper** - DTO dönüşümleri
- **DataTables** - Sayfalama
- **File Upload** - Form file handling
- **User Context** - Kullanıcı bilgileri

### 📦 **Paket Versiyonları:**

```xml
<PackageVersion Include="Asp.Versioning.Http" Version="8.1.0" />
<PackageVersion Include="Asp.Versioning.Mvc.ApiExplorer" Version="8.1.0" />
<PackageVersion Include="Bogus" Version="35.6.1" />
<PackageVersion Include="FluentValidation" Version="11.10.0" />
<PackageVersion Include="FluentValidation.DependencyInjectionExtensions" Version="11.10.0" />
<PackageVersion Include="AspectCore.Abstractions" Version="2.4.0" />
<PackageVersion Include="FluentMigrator" Version="6.2.0" />
<PackageVersion Include="FluentMigrator.Runner" Version="6.2.0" />
<PackageVersion Include="FluentMigrator.Runner.SqlServer" Version="6.2.0" />
<PackageVersion Include="Maggsoft.Cache" Version="2.0.19" />
<PackageVersion Include="Maggsoft.Cache.MemoryCache" Version="2.0.7" />
<PackageVersion Include="Maggsoft.Core" Version="2.0.35" />
<PackageVersion Include="Maggsoft.Data" Version="2.0.15" />
<PackageVersion Include="Maggsoft.Data.Mssql" Version="2.0.10" />
<PackageVersion Include="Maggsoft.Endpoints" Version="2.0.6" />
<PackageVersion Include="Maggsoft.Framework" Version="2.2.28" />
<PackageVersion Include="Maggsoft.Mssql" Version="2.0.10" />
<PackageVersion Include="Maggsoft.Mssql.Services" Version="2.0.16" />
<PackageVersion Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.10" />
<PackageVersion Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.10" />
<PackageVersion Include="Microsoft.EntityFrameworkCore" Version="8.0.10" />
<PackageVersion Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.10" />
<PackageVersion Include="Serilog.AspNetCore" Version="8.0.3" />
<PackageVersion Include="Swashbuckle.AspNetCore" Version="6.9.0" />
```

### 🎯 **MinimalAirbnb için Öğrenilen Dersler:**

#### **1. Proje Yapısı:**
- **Clean Architecture** - Katmanlı mimari
- **Separation of Concerns** - Sorumlulukların ayrılması
- **Modular Design** - Modüler tasarım
- **Scalable Structure** - Ölçeklenebilir yapı

#### **2. API Design Patterns:**
- **Minimal API Endpoints** - Minimal API kullanımı
- **Endpoint Groups** - Endpoint gruplandırma
- **Authorization Policies** - Rol bazlı yetkilendirme
- **Validation Filters** - Endpoint validation
- **File Upload Handling** - Dosya yükleme

#### **3. Service Layer Patterns:**
- **Repository Pattern** - Repository deseni
- **Caching Strategy** - Cache stratejisi
- **Event Publishing** - Event publishing
- **AutoMapper Integration** - AutoMapper entegrasyonu
- **FluentValidation** - Validation

#### **4. Web Layer Patterns:**
- **HTTP Client Abstraction** - HTTP client soyutlama
- **Cookie Authentication** - Cookie authentication
- **Session Management** - Session yönetimi
- **Base Controller** - Base controller
- **Message Service** - Mesaj servisi

#### **5. Security Patterns:**
- **JWT Bearer Tokens** - JWT token'ları
- **Role-based Authorization** - Rol bazlı yetkilendirme
- **Claims-based Security** - Claim bazlı güvenlik
- **Custom Identity** - Özel identity
- **Encryption Service** - Şifreleme servisi

#### **6. Infrastructure Patterns:**
- **FluentMigrator** - Database migration
- **Serilog** - Logging
- **Swagger/OpenAPI** - API documentation
- **API Versioning** - API versiyonlama
- **File Management** - Dosya yönetimi

### 💡 **MinimalAirbnb için Uygulanacak Özellikler:**

#### **1. Gelişmiş Entity Yapısı:**
- **Soft Delete** - Yumuşak silme
- **Audit Fields** - Denetim alanları
- **Navigation Properties** - İlişki özellikleri
- **Factory Methods** - Factory metodları

#### **2. Kapsamlı DTO Yapısı:**
- **Add/Edit/Result DTOs** - CRUD DTO'ları
- **File Upload Support** - Dosya yükleme desteği
- **Validation Attributes** - Validation attribute'ları
- **JSON Converters** - JSON dönüştürücüler

#### **3. Gelişmiş Service Layer:**
- **Caching Strategy** - Cache stratejisi
- **Event Publishing** - Event publishing
- **AutoMapper Integration** - AutoMapper entegrasyonu
- **DataTables Support** - DataTables desteği

#### **4. Modern API Design:**
- **Minimal API Endpoints** - Minimal API
- **Endpoint Groups** - Endpoint grupları
- **Authorization Policies** - Yetkilendirme politikaları
- **Validation Filters** - Validation filtreleri

#### **5. Robust Web Layer:**
- **HTTP Client Abstraction** - HTTP client soyutlama
- **Base Controller** - Base controller
- **Message Service** - Mesaj servisi
- **Session Management** - Session yönetimi

#### **6. Enterprise Features:**
- **Multi-tenancy** - Çok kiracılılık (Company-based)
- **File Management** - Dosya yönetimi
- **Background Services** - Arka plan servisleri
- **SMS Integration** - SMS entegrasyonu
- **Email Services** - Email servisleri

### 🚀 **Sonuç:**
Afitap SSA projesi, MinimalAirbnb için mükemmel bir referans modeldir. Clean Architecture, modern .NET 8 özellikleri, Maggsoft framework entegrasyonu ve enterprise-level patterns kullanılarak geliştirilmiştir. Bu projeden öğrenilen patterns ve best practices, MinimalAirbnb projesinin daha güçlü, ölçeklenebilir ve maintainable olmasını sağlayacaktır.


# Maggsoft Nuget Paketleri
  - dotnet add package Maggsoft.Core --version 2.0.39
  - dotnet add package Maggsoft.Data --version 2.0.20
  - dotnet add package Maggsoft.Framework --version 2.3.6
  - dotnet add package Maggsoft.Data.Mssql --version 2.0.10
  - dotnet add package Maggsoft.Mssql --version 2.0.12
  - dotnet add package Maggsoft.Cache --version 2.0.22
  - dotnet add package Maggsoft.Cache.MemoryCache --version 2.0.7
  - dotnet add package Maggsoft.Services --version 2.0.8
  - dotnet add package Maggsoft.Aspect.Core --version 1.0.15
  - dotnet add package Maggsoft.Mssql.Services --version 2.0.17
  - dotnet add package Maggsoft.Endpoints --version 2.0.7
  - dotnet add package Maggsoft.Sqlite --version 2.0.8
  - dotnet add package Maggsoft.Data.Sqlite --version 2.0.12
  - dotnet add package Maggsoft.Sqlite.Services --version 2.0.10
  - dotnet add package Maggsoft.Ocelot.Core --version 1.0.6
  - dotnet add package Maggsoft.Data.Npgsql --version 1.0.11
  - dotnet add package Maggsoft.Cache.Redis --version 2.0.3
  - dotnet add package Maggsoft.Data.Mongo --version 1.0.3
  - dotnet add package Maggsoft.EventBus.IoC --version 1.0.16
  - dotnet add package Maggsoft.EventBus.RabbitMQ --version 1.0.17
  - dotnet add package Maggsoft.EventBus.AzureServiceBus --version 1.0.15
  - dotnet add package Maggsoft.Dto.Sqlite --version 2.0.2
  - dotnet add package Maggsoft.Dto.Mongo --version 1.0.2
  - dotnet add package Maggsoft.Dto.Npgsql --version 1.0.2,
  - dotnet add package Maggsoft.Mongo.Services --version 1.0.8
  - dotnet add package Maggsoft.Dto.Mssql --version 1.0.2
  - dotnet add package Maggsoft.Npgsql.Services --version 1.0.8
  - dotnet add package Maggsoft.EventBus --version 1.0.19
  - dotnet add package Maggsoft.Npgsql --version 1.0.5
  - dotnet add package Maggsoft.Logging --version 1.0.6
  - dotnet add package Maggsoft.Mongo --version 1.0.5
