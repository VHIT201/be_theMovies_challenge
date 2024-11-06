CREATE TABLE "favoriteFilm"
(
    "Id" uuid NOT NULL,
    "userid" uuid,
    "movieid" varchar(255),
    "media_type" varchar(255),
    "CreatedAt" timestamp with time zone,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "favoriteFilm_pkey" PRIMARY KEY ("Id")
)

