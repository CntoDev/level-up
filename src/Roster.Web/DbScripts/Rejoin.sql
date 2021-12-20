START TRANSACTION;

ALTER TABLE "MemberDischarge" ADD "IsAlumni" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20211218122326_Rejoin', '6.0.1');

COMMIT;

