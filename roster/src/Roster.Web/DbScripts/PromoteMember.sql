START TRANSACTION;

ALTER TABLE "Members" ADD "RankId" integer NULL;

CREATE TABLE "Ranks" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_Ranks" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211203185731_PromoteMember', '5.0.9');

COMMIT;

