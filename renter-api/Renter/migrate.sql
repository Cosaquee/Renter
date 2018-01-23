CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Authors" (
        "Id" bigserial NOT NULL,
        "Description" text NULL,
        "Name" varchar(32) NOT NULL,
        "Surname" varchar(32) NOT NULL,
        CONSTRAINT "PK_Authors" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Categories" (
        "Id" bigserial NOT NULL,
        "Name" varchar(64) NOT NULL,
        CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Directors" (
        "Id" bigserial NOT NULL,
        "Name" varchar(32) NOT NULL,
        "Surname" varchar(32) NOT NULL,
        CONSTRAINT "PK_Directors" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "RefreshTokens" (
        "Id" text NOT NULL,
        "Expire" timestamp NOT NULL,
        "UserId" text NULL,
        CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Roles" (
        "Id" bigserial NOT NULL,
        "Name" varchar(32) NOT NULL,
        CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Subscriptions" (
        "Id" bigserial NOT NULL,
        "Cost" numeric NOT NULL,
        "Name" text NULL,
        "Seconds" float8 NOT NULL,
        CONSTRAINT "PK_Subscriptions" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Books" (
        "Id" bigserial NOT NULL,
        "AuthorId" int8 NOT NULL,
        "CategoryId" int8 NOT NULL,
        "CoverURL" text NULL,
        "Description" text NULL,
        "ISBN" varchar(13) NOT NULL,
        "Rented" bool NOT NULL,
        "Title" varchar(128) NOT NULL,
        CONSTRAINT "PK_Books" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Books_Authors_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "Authors" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Books_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Movies" (
        "Id" bigserial NOT NULL,
        "CategoryId" int8 NOT NULL,
        "CoverURL" text NULL,
        "Description" text NULL,
        "DirectorId" int8 NOT NULL,
        "Seconds" float8 NOT NULL,
        "Title" varchar(128) NOT NULL,
        CONSTRAINT "PK_Movies" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Movies_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Movies_Directors_DirectorId" FOREIGN KEY ("DirectorId") REFERENCES "Directors" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "Users" (
        "Id" varchar(256) NOT NULL,
        "Email" varchar(64) NOT NULL,
        "Name" text NOT NULL,
        "Password" varchar(64) NOT NULL,
        "ProvileAvatar" text NULL,
        "RoleId" int8 NOT NULL,
        "Surname" text NOT NULL,
        "UserName" varchar(64) NOT NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "BooksRatings" (
        "Id" bigserial NOT NULL,
        "BookId" int8 NULL,
        "ISBN" text NULL,
        "Rate" int4 NOT NULL,
        "UserId" varchar(256) NULL,
        CONSTRAINT "PK_BooksRatings" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_BooksRatings_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE NO ACTION,
        CONSTRAINT "FK_BooksRatings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE NO ACTION
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "MovieRatings" (
        "Id" bigserial NOT NULL,
        "MovieId" int8 NOT NULL,
        "Rate" int4 NOT NULL,
        "UserId" varchar(256) NULL,
        CONSTRAINT "PK_MovieRatings" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_MovieRatings_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES "Movies" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_MovieRatings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE NO ACTION
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "RentBooks" (
        "Id" bigserial NOT NULL,
        "BookId" int8 NOT NULL,
        "From" timestamp NOT NULL,
        "Received" bool NOT NULL,
        "To" timestamp NOT NULL,
        "UserId" varchar(256) NOT NULL,
        CONSTRAINT "PK_RentBooks" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_RentBooks_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_RentBooks_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "RentMovies" (
        "Id" bigserial NOT NULL,
        "From" timestamp NOT NULL,
        "MovieId" int8 NOT NULL,
        "To" timestamp NOT NULL,
        "UserId" varchar(256) NOT NULL,
        CONSTRAINT "PK_RentMovies" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_RentMovies_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES "Movies" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_RentMovies_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE TABLE "UserSubscriptions" (
        "Id" bigserial NOT NULL,
        "StartDate" timestamp NOT NULL,
        "SubscriptionId" int4 NOT NULL,
        "SubscriptionId1" int8 NULL,
        "UserId" varchar(256) NULL,
        CONSTRAINT "PK_UserSubscriptions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserSubscriptions_Subscriptions_SubscriptionId1" FOREIGN KEY ("SubscriptionId1") REFERENCES "Subscriptions" ("Id") ON DELETE NO ACTION,
        CONSTRAINT "FK_UserSubscriptions_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE NO ACTION
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_Books_AuthorId" ON "Books" ("AuthorId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_Books_CategoryId" ON "Books" ("CategoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_BooksRatings_BookId" ON "BooksRatings" ("BookId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_BooksRatings_UserId" ON "BooksRatings" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_MovieRatings_MovieId" ON "MovieRatings" ("MovieId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_MovieRatings_UserId" ON "MovieRatings" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_Movies_CategoryId" ON "Movies" ("CategoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_Movies_DirectorId" ON "Movies" ("DirectorId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_RentBooks_BookId" ON "RentBooks" ("BookId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_RentBooks_UserId" ON "RentBooks" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_RentMovies_MovieId" ON "RentMovies" ("MovieId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_RentMovies_UserId" ON "RentMovies" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_Users_RoleId" ON "Users" ("RoleId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_UserSubscriptions_SubscriptionId1" ON "UserSubscriptions" ("SubscriptionId1");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    CREATE INDEX "IX_UserSubscriptions_UserId" ON "UserSubscriptions" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120124100_InitialMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180120124100_InitialMigration', '2.0.1-rtm-125');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120141000_ResizedCoverUrl') THEN
    ALTER TABLE "Books" ADD "ResizedCoverURL" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20180120141000_ResizedCoverUrl') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20180120141000_ResizedCoverUrl', '2.0.1-rtm-125');
    END IF;
END $$;
