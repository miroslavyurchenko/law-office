drop database law_office;
create database if not exists law_office;
use law_office;

create table lawyers(
	id int primary key auto_increment,
    full_name varchar(100) not null,
    experience int not null default 0,
    license varchar(15) not null,
    contact_data text,
    type_of_case varchar(50)
);

create table clients(
	id int primary key auto_increment,
    full_name varchar(100) not null,
    date_of_birth date,
    rnokpp varchar(20) unique not null,
    contact_data text
);

create table service (
    id int primary key auto_increment,
    cost decimal(10,2) not null,
    deadline varchar(100),
    type_of_service varchar(50)
);
create table status_s (
    id int primary key auto_increment,
    `status` varchar(50)
);

create table sale (
    id int primary key auto_increment,
    clients_id int,
    lawyers_id int,
    sale_date TIMESTAMP default CURRENT_TIMESTAMP(),
    FOREIGN KEY (lawyers_id) references lawyers(id)
);

ALTER TABLE sale
ADD COLUMN status_id INT,
ADD CONSTRAINT fk_status FOREIGN KEY (status_id) REFERENCES status_s(id);

create table type_of_service(
	id int primary key auto_increment,
    type_of_service varchar(50),
    `name` varchar(50),
    cost decimal(10,2) not null
);
CREATE TABLE users (
    id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL
);