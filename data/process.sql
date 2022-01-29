CREATE TABLE "RecruitmentSaga" (
    "CorrelationId" uuid NOT NULL,
    "Nickname" text NULL,
    "AutomaticDischarge" boolean NOT NULL,
    "RecruitmentStartDate" timestamp with time zone NULL,
    "ModsCheckDate" timestamp with time zone NULL,
    "BootcampCompletionDate" timestamp with time zone NULL,
    "EnoughAttendedEvents" boolean NOT NULL,
    "TrialSucceeded" boolean NULL,
    CONSTRAINT "PK_RecruitmentSaga" PRIMARY KEY ("CorrelationId")
);


