
-- =============================================================================
-- CITIES
-- =============================================================================

CREATE TABLE city (
    id INTEGER PRIMARY KEY,
    name VARCHAR(64)
);

INSERT INTO city (
    id, name
)
VALUES
    ( 1, 'Piltover' ),
    ( 2, 'Zaun' )
;

-- =============================================================================
-- PERSONS
-- =============================================================================

CREATE TABLE person (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(64),
    age INTEGER,
    city_id INTEGER,
    FOREIGN KEY(city_id) REFERENCES city(id)
);

INSERT INTO person (
    name, age, city_id
)
VALUES
    ( 'Vi', 22, 2 ),
    ( 'Powder', 17, 2 ),
    ( 'Caitlyn', 23, 1 )
;
