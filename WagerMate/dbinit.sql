CREATE TABLE "user"
(
    "id"       serial PRIMARY KEY,
    "name"     varchar,
    "email"    varchar,
    "password" varchar
);

CREATE TABLE "userbet"
(
    "id"      serial PRIMARY KEY,
    "user_id" int,
    "bet_id"  int,
    "amount"  double precision,
    "case_id" int
);

CREATE TABLE "winner"
(
    "id"         serial PRIMARY KEY,
    "bet_id"     int,
    "userbet_id" int,
    "amount"     double precision
);

CREATE TABLE "bet"
(
    "id"              serial PRIMARY KEY,
    "title"           varchar,
    "description"     varchar,
    "invitation_code" varchar,
    "access"          int,
    "state"           int,
    "created"         date,
    "expiration"      date
);

CREATE TABLE "case"
(
    "id"       serial PRIMARY KEY,
    "bet_id"   integer,
    "casetype" varchar
);

ALTER TABLE "userbet"
    ADD FOREIGN KEY ("bet_id") REFERENCES "bet" ("id");

ALTER TABLE "userbet"
    ADD FOREIGN KEY ("user_id") REFERENCES "user" ("id");

ALTER TABLE "userbet"
    ADD FOREIGN KEY ("case_id") REFERENCES "case" ("id");

ALTER TABLE "winner"
    ADD FOREIGN KEY ("bet_id") REFERENCES "bet" ("id");

ALTER TABLE "winner"
    ADD FOREIGN KEY ("userbet_id") REFERENCES "userbet" ("id");

ALTER TABLE "case"
    ADD FOREIGN KEY ("bet_id") REFERENCES "bet" ("id");