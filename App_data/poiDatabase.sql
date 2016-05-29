CREATE TABLE siteType (
	id_siteType integer,
	rss boolean,
	html boolean
);

CREATE TABLE enderecosURL (
	id_enderecosURL integer,
	url integer,
	fk_siteType integer
);

CREATE TABLE poi (
	id_poi integer,
	title varchar,
	description varchar,
	url varchar,
	date date
);

CREATE TABLE poiEngine (
	id_poiEngine integer,
	fk_endereçosURL integer,
	fk_poi integer
);

