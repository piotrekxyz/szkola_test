create table Instrumenty(Id tinyint primary key identity(1,1), Nazwa varchar(20) not null, Sekcja varchar(50))

create table Uczniowie(Id smallint primary key identity(1,1),
Imie varchar(20) not null, Nazwisko varchar(30) not null,
Cykl tinyint not null constraint sprawdz_cykl check(Cykl = 4 or Cykl = 6),
Klasa tinyint not null, Instrument varchar(20), Nauczyciel varchar(20), Pesel varchar(11) not null unique,
Absolwent tinyint constraint sprawdz_absolwenta check(Absolwent = 0 or Absolwent = 1),
CzasDodania datetime2(0) default getdate())

Create table Pracownicy(Id smallint primary key identity(1,1), Imie varchar(20) not null,
Nazwisko varchar(30) not null, Stanowisko varchar(40) not null,
Pesel varchar(11) not null unique, CzasDodania datetime2(0) default getdate())

create table Przedmioty(Id tinyint primary key identity(1,1), Nazwa varchar(30) not null, Opis text)

create table Nauczyciele(Id tinyint primary key identity(1,1), Imie varchar(20) not null,
Nazwisko varchar(30) not null, Pesel varchar(11), Instrument varchar(20),
Przedmioty varchar(100))