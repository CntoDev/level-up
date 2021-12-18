START TRANSACTION;

ALTER TABLE "Members" ADD "Discharged" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211218090633_DischargeSystem2', '6.0.1');

COMMIT;

