START TRANSACTION;

ALTER TABLE "Members" ADD "JoinDate" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211204075040_JoinDate', '5.0.9');

COMMIT;

