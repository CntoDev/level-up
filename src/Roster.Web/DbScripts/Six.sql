START TRANSACTION;

ALTER TABLE "Members" ALTER COLUMN "VerificationTime" TYPE timestamp with time zone;

ALTER TABLE "Members" ALTER COLUMN "JoinDate" TYPE timestamp with time zone;

ALTER TABLE "Members" ALTER COLUMN "DateOfBirth" TYPE timestamp with time zone;

ALTER TABLE "EventStates" ALTER COLUMN "EventDate" TYPE timestamp with time zone;

ALTER TABLE "ApplicationForms" ALTER COLUMN "DateOfBirth" TYPE timestamp with time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211216070910_Six', '6.0.1');

COMMIT;

