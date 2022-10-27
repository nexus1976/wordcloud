START TRANSACTION;

DO $$
BEGIN
	CREATE TABLE IF NOT EXISTS "Comments" (
		id uuid NOT NULL,
		commentdate timestamp without time zone NOT NULL DEFAULT (timezone('utc'::text, now())),
		commenttext character varying NOT NULL,
		commentparsed character varying NOT NULL,
		CONSTRAINT "PK_Comments" PRIMARY KEY (id)
	);
END $$;

DO $$
BEGIN
	CREATE INDEX IF NOT EXISTS "IX_Comments_commentdate" ON "Comments" (commentdate);
END $$;

DO $$
BEGIN
	CREATE TABLE IF NOT EXISTS "WordCounts" (
		id uuid NOT NULL,
		word character varying(1024) NOT NULL,
		wordcount int8 NOT NULL,
		CONSTRAINT "PK_WordCounts" PRIMARY KEY (id)
	);
END $$;

DO $$
BEGIN
	CREATE INDEX IF NOT EXISTS "IX_WordCounts_word" ON "WordCounts" (word);
END $$;

COMMIT;