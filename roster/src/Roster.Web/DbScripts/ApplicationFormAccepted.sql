START TRANSACTION;

ALTER TABLE "ApplicationForms" ALTER COLUMN "Accepted" DROP NOT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211209174528_ApplicationFormAccepted', '5.0.9');

COMMIT;

