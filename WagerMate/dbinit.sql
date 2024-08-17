-- Create ENUM types first
CREATE TYPE access_enum AS ENUM ('public', 'private', 'restricted');
CREATE TYPE state_enum AS ENUM ('pending', 'active', 'closed');

-- Create the "users" table
CREATE TABLE "users"
(
    "id"       serial PRIMARY KEY,
    "name"     varchar,
    "email"    varchar,
    "password" varchar
);

-- Create the "wageritem" table
CREATE TABLE "wageritem"
(
    "id"     serial PRIMARY KEY,
    "money"  bool,
    "amount" double precision,
    "item"   varchar
);

-- Create the "wagers" table
CREATE TABLE "wagers"
(
    "id"           serial PRIMARY KEY,
    "wageritem_id" integer,
    "description"  varchar,
    "cases"        varchar[],
    "access"       access_enum,
    "state"        state_enum,
    "created"      date,
    "expiration"   date,
    FOREIGN KEY ("wageritem_id") REFERENCES "wageritem" ("id")
);

-- Create the "userwagers" table
CREATE TABLE "userwagers"
(
    "id"       serial PRIMARY KEY,
    "user_id"  int,
    "wager_id" int,
    FOREIGN KEY ("user_id") REFERENCES "users" ("id"),
    FOREIGN KEY ("wager_id") REFERENCES "wagers" ("id")
);