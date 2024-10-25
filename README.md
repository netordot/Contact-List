# Contact-List
## О проекте
### Технологии
+ ASP.NET Core
+ Entity Framework Core
+ Docker
+ PostgreSQL

### Библиотеки
+ FluentValidation
+ CSharpFunctionalExtensions

### Архитектура
Проект разделен на следующие слои: Domain, Persistence, Application, API

В слое Application происходит валидация входящих Commands, также используется подход "одна фича - один хендлер (сервис)".

### FluentValidation
Для валидации ValueObjects был создан метод расширения MustBeValueObject, который валидирует согласно фабричным методам у объектов-значений. VO Email и PhoneNumber проверяются с помощью Regex. Для мобильного номера написан Regex для российского региона. Email имеет общий формат: name@somemail.something.

### CSharpFunctionalExtensions
Используется Result pattern. В качестве класса Error используется самописный класс Error.

### DDD
В домене используется богатая доменная модель. Агрегатом является сущность контакта. Используются объекты-значения: PhoneNumber и Email.

## Инструкция по развертыванию (удаленный сервер)
1. Убедиться в наличии доступа
пример: ssh имя_пользователя@ip_сервера

2.	Установка docker: 
sudo apt-get update
sudo apt-get install -y docker.io
sudo systemctl start docker
sudo systemctl enable docker

3.	Установка docker-compose:
 sudo curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
docker-compose –version

4.	Клонирование репозитория (git clone https://github.com/netordot/Contact-List.git cd Contact-List), p.s. docker-compose лежит в корне проекта
   
5.	Создать docker образ приложения docker build -t contactlist-api .
   
6.	Убедиться, что docker-compose лежит в корне проекта, запустить docker-compose up -d
   
7.	Проверить, что все работает : docker ps

