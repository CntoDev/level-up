START TRANSACTION;

ALTER TABLE "RecruitmentSaga" ALTER COLUMN "RecruitmentStartDate" TYPE timestamp with time zone;

ALTER TABLE "RecruitmentSaga" ALTER COLUMN "ModsCheckDate" TYPE timestamp with time zone;

ALTER TABLE "RecruitmentSaga" ALTER COLUMN "BootcampCompletionDate" TYPE timestamp with time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211216175735_Six', '6.0.1');

COMMIT;

