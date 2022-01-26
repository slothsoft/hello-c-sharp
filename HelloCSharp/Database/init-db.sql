
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
    id INTEGER PRIMARY KEY,
    name VARCHAR(64),
    age INTEGER,
    city_id INTEGER,
    FOREIGN KEY(city_id) REFERENCES city(id)
);

INSERT INTO person (
    id, name, age, city_id
)
VALUES
    ( 1, 'Vi', 22, 2 ),
    ( 2, 'Powder', 17, 2 ),
    ( 3, 'Caitlyn', 23, 1 ),
    ( 4, 'Silco', 48, 2 ),
    ( 5, 'Ekko', 18, 2 )
;

-- =============================================================================
-- RELATIONSHIPS
-- =============================================================================

CREATE TABLE relationship (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    type VARCHAR(64),
    from_id INTEGER,
    to_id INTEGER,
    FOREIGN KEY(from_id) REFERENCES person(id),
    FOREIGN KEY(to_id) REFERENCES person(id)
);

INSERT INTO relationship (
    from_id, type, to_id
)
VALUES
    ( 1, 'Siblings', 2 ),
    ( 1, 'Partners', 3 ),
    ( 2, 'Hates', 3 ),
    ( 4, 'ParentOf', 2 )
;
