CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "RecruitmentSaga" (
    "CorrelationId" uuid NOT NULL,
    "Nickname" text NULL,
    "RecruitmentStartDate" timestamp without time zone NULL,
    "ModsCheckDate" timestamp without time zone NULL,
    "BootcampCompletionDate" timestamp without time zone NULL,
    "EnoughAttendedEvents" boolean NOT NULL,
    "TrialSucceeded" boolean NULL,
    CONSTRAINT "PK_RecruitmentSaga" PRIMARY KEY ("CorrelationId")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211205094209_RecruitmentSaga', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE "RecruitmentSaga" ADD "AutomaticDischarge" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211206174619_AutomaticDischarge', '5.0.9');

COMMIT;

