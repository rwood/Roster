{Version1}
CREATE TABLE "Version" ("Version" INTEGER PRIMARY KEY, "CreateDate" DATETIME NOT NULL  DEFAULT CURRENT_TIMESTAMP, "ChangeDate" DATETIME NOT NULL  DEFAULT CURRENT_TIMESTAMP); 
CREATE TRIGGER "TR_Version" AFTER UPDATE ON Version FOR EACH ROW  BEGIN UPDATE Version SET ChangeDate = DATETIME() WHERE Version = old.Version; END;
INSERT INTO Version ("Version") VALUES (1);
{Version2}
CREATE TABLE "EnrollmentTags" ("Tag" VARCHAR NOT NULL, "CreateDate" DATETIME NOT NULL  DEFAULT CURRENT_TIMESTAMP, "ChangeDate" DATETIME NOT NULL  DEFAULT CURRENT_TIMESTAMP, PRIMARY KEY ("Tag"));
CREATE TRIGGER "TR_EnrollmentTags" AFTER UPDATE ON EnrollmentTags FOR EACH ROW  BEGIN UPDATE EnrollmentTags SET ChangeDate = DATETIME() WHERE Tag = old.Tag; END;
ALTER TABLE "Enrollments" ADD COLUMN "Tag" VARCHAR;
INSERT INTO EnrollmentTags ("Tag") VALUES ("Regular");
{Version3}
INSERT INTO EnrollmentTags ("Tag") VALUES ("Millcreek Student");
INSERT INTO EnrollmentTags ("Tag") VALUES ("Fee Waiver");
INSERT INTO EnrollmentTags ("Tag") VALUES ("Bob Green");
INSERT INTO EnrollmentTags ("Tag") VALUES ("Super Senior");
INSERT INTO EnrollmentTags ("Tag") VALUES ("Youth in Custody");
UPDATE Enrollments SET Tag = "Millcreek Student", FeesPaid = 0 WHERE FeesPaid = 0;
UPDATE Enrollments SET Tag = "Fee Waiver", FeesPaid = 0 WHERE FeesPaid = 11;
UPDATE Enrollments SET Tag = "Bob Green", FeesPaid = 0 WHERE FeesPaid = 33;
UPDATE Enrollments SET Tag = "Super Senior", FeesPaid = 0 WHERE FeesPaid = 55;
UPDATE Enrollments SET Tag = "Youth in Custody", FeesPaid = 0 WHERE FeesPaid = 77;