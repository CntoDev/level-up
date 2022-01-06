START TRANSACTION;

ALTER TABLE "ApplicationForms" ADD "PreferredPronouns" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220106084758_Pronoun', '6.0.1');

COMMIT;

START TRANSACTION;

ALTER TABLE "ApplicationForms" ADD "TimeZone" text NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220106153621_Timezone', '6.0.1');

COMMIT;

START TRANSACTION;

ALTER TABLE "ApplicationForms" ADD "LanguageSkillLevel" integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220106154934_LanguageSkillLevel', '6.0.1');

COMMIT;

