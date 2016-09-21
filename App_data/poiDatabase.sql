--
-- File generated with SQLiteStudio v3.0.7 on sáb Jun 11 16:46:52 2016
--
-- Text encoding used: windows-1252
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: poiEngine
CREATE TABLE poiEngine (id_poiEngine INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, fk_endercosURL INTEGER REFERENCES enderecosURL (id_enderecosURL), fk_poi INTEGER REFERENCES poi (id_poi));

-- Table: poi
CREATE TABLE poi (
    id_poi      INTEGER        PRIMARY KEY AUTOINCREMENT
                               UNIQUE
                               NOT NULL,
    title       VARCHAR (500),
    description VARCHAR (1500),
    url         VARCHAR (100),
    date        DATE
);

-- Table: enderecosURL
CREATE TABLE enderecosURL (
    id_enderecosURL INTEGER       PRIMARY KEY AUTOINCREMENT
                                  UNIQUE
                                  NOT NULL,
    url             VARCHAR (500),
    fk_siteType     INTEGER       REFERENCES siteType (id_siteType) ON DELETE CASCADE
                                  NOT NULL
);
INSERT INTO enderecosURL (id_enderecosURL, url, fk_siteType) VALUES (1, 'http://www.formacaosaude.com/categoria/formacao/feed/', 2);

-- Table: siteType
CREATE TABLE siteType (id_siteType INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, type TEXT NOT NULL CHECK (type IN ('rss', 'html', 'search_engine')) DEFAULT html);
INSERT INTO siteType (id_siteType, type) VALUES (1, 'html');
INSERT INTO siteType (id_siteType, type) VALUES (2, 'rss');
INSERT INTO siteType (id_siteType, type) VALUES (3, 'search_engine');

-- Table: poiCategories
CREATE TABLE poiCategories (
    id_poiCategories INTEGER PRIMARY KEY AUTOINCREMENT
                             UNIQUE
                             NOT NULL,
    fk_categories      INTEGER REFERENCES categories (id_categories),
    fk_poi           INTEGER REFERENCES poi (id_poi) 
);

-- Table: categories
CREATE TABLE categories (
    id_categories INTEGER PRIMARY KEY AUTOINCREMENT
                             UNIQUE
                             NOT NULL,
    description      varchar(300) 
);

-- Table: searchKeywords
INSERT INTO searchKeywords (id_searchKeywords, searchQuery) VALUES (1, 'cursos profissionais de medicina alternativa Porto');
CREATE TABLE searchKeywords (id_searchKeywords INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, searchQuery TEXT NOT NULL);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
