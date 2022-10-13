
proje 2 microservice den oluşmaktadır
 1. ContactMicroservice localhost:5000
   vertabanı : Postgresql contactdb
   açıklama: Proje üzerinden kişi ekleme,silme işlemleri yapılabilinir, Proje içerisinde varolan kişilerin üzerine iletişim bilgileri eklenip silinebilinir.
   propje üzerinden tüm kişiler listelenebilinir

 2. ReportMicroservice localhost:5001
    veritabanı: Postgresql reportdb
    açıklama: Proje üzerinden rapor isteği yapılır ve bu istek kaydedilip uygun zamanda işlenmek üzere messagebroker a iletilir. Proje messagebrokerdan isteği alır ve ContactMicroservice 
    den gerekli dataları çekip excel dosyasına kayıt eder. Rapor isteğin yerine getirildiği şeklinde işaretlenir ve rapora excel dosyasının path i eklenir.

 3. Pstgresql içerisinde iki tane veri tabanı bulunur bunlar iki microservice için oluşturulur. 

 4. Rabbitmq iki proje arasında messagebroker görevini üstlenir.


 Kurulum

 Proje Docker desktop a ihtiyaç duyar
 1- Projeyi indirin
 2- Startup project olarak "docker-compose" olarak seçilir
 3- Proje çalıştırılır. Proje çalıştığında docker kurulumları , database migration ve örnek datalar otomatik eklenir.

 not: AddContact methodu Contacttype olarak enum kullanır. 
 contacttype enum:
   Phone = 1,
   Email = 2,
   Location = 3
   
   ![report_diagram](https://user-images.githubusercontent.com/23095498/195560966-a4a53b7d-1423-422e-8362-9b1dbc0fd469.jpg)

   
