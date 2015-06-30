CREATE OR REPLACE 
FUNCTION beschikbaaremail
(
email "ACCOUNT"."email"%TYPE
)
RETURN VARCHAR2
AS
ingebruikemail VARCHAR2(100);
BEGIN
	SELECT "email" INTO ingebruikemail FROM "ACCOUNT" WHERE "email" = email;
	RETURN 'Fout';
	EXCEPTION
	WHEN NO_DATA_FOUND THEN
	RETURN 'Goed';
END;
/
CREATE OR REPLACE 
FUNCTION beschikbaarnaam 
(
gebruikernaam "ACCOUNT"."gebruikersnaam"%TYPE
)
RETURN VARCHAR2
AS
ingebruiknaam VARCHAR2(100);
BEGIN
	SELECT "gebruikersnaam" INTO ingebruiknaam FROM "ACCOUNT" WHERE "gebruikersnaam" = gebruikernaam;
	RETURN 'Fout';
	EXCEPTION
	WHEN NO_DATA_FOUND THEN
	RETURN 'Goed';
END;
/
CREATE OR REPLACE 
FUNCTION CHECKBANKNR
(
banknr VARCHAR2
) 
RETURN VARCHAR2
AS
BEGIN
	IF regexp_like(banknr, '[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}') THEN
		RETURN 'Goed';
	END IF;
	RETURN 'Fout';
END;
/
CREATE OR REPLACE 
PROCEDURE checkbeschikbaar
(
gebruikersnaam "ACCOUNT"."gebruikersnaam"%TYPE,
email "ACCOUNT"."email"%TYPE,
text out VARCHAR2
)
AS
errortxt VARCHAR2(2000);
BEGIN
	IF BESCHIKBAARNAAM(gebruikersnaam) = 'Goed' THEN
		IF BESCHIKBAAREMAIL(email) = 'Goed' THEN
			errortxt := 'Goed';
			text := errortxt;
		ELSE
			errortxt := 'e-mail in gebruik';
			text := errortxt;
		END IF;
	ELSE
		errortxt := 'gebruikernaam in gebruik';
		text := errortxt;
	END IF;
END;

/
CREATE OR REPLACE 
FUNCTION CHECKEMAIL 
(
email VARCHAR2
) 
RETURN VARCHAR2
AS
BEGIN
	IF regexp_like(email, '\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*') THEN
		RETURN 'Goed';
	END IF;
	RETURN 'Fout';
END;
/
CREATE OR REPLACE 
FUNCTION checkhuisnr
(
huisnummer VARCHAR2
) 
RETURN VARCHAR2
AS
BEGIN
	IF regexp_like(huisnummer, '([0-9]{1,4})([A-z]{1})?') THEN
		RETURN 'Goed';
	END IF;
	RETURN 'Fout';
END;
/
CREATE OR REPLACE 
FUNCTION checkpostcode 
(
postcode VARCHAR2
) 
RETURN VARCHAR2
AS
BEGIN
	IF regexp_like(postcode, '^[1-9][0-9]{3}\s?[a-zA-Z]{2}$') THEN
		RETURN 'Goed';
	END IF;
	RETURN 'Fout';
END;
/
CREATE OR REPLACE 
FUNCTION checktekst
(
tekst VARCHAR2
) 
RETURN VARCHAR2
AS
BEGIN
	IF regexp_like(tekst, '^([A-z])*$') THEN
		RETURN 'Goed';
	END IF;
	RETURN 'Fout';
END;
/
CREATE OR REPLACE 
PROCEDURE hoofdinschrijving
(
voornaam PERSOON."voornaam"%TYPE,
tussenvoegsel persoon."tussenvoegsel"%TYPE,
achternaam PERSOON."achternaam"%TYPE,
straat PERSOON."straat"%TYPE,
huisnr PERSOON."huisnr"%TYPE,
woonplaats PERSOON."woonplaats"%TYPE,
banknr PERSOON."banknr"%TYPE,
email "ACCOUNT"."email"%TYPE,
gebruiker "ACCOUNT"."gebruikersnaam"%TYPE,
actievatiehash "ACCOUNT"."activatiehash"%TYPE,
text out VARCHAR2
)
AS
errortext VARCHAR2(2000);
BEGIN
	IF CHECKTEKST(voornaam) = 'Goed' THEN
		dbms_output.put_line('voor');
		IF CHECKTEKST(achternaam) = 'Goed' THEN
			dbms_output.put_line('achter');		
			IF CHECKTEKST(straat) = 'Goed' THEN
				dbms_output.put_line('straat');	
				IF  CHECKPOSTCODE(woonplaats) = 'Goed' THEN
					dbms_output.put_line('woonplaats');	
					IF CHECKHUISNR(huisnr) = 'Goed' THEN
						dbms_output.put_line('huisnr');	
						IF CHECKTEKST(gebruiker) = 'Goed' THEN
							dbms_output.put_line('gebruiker');	
							IF CHECKEMAIL(email) = 'Goed' THEN
								dbms_output.put_line('email');
								IF CHECKBANKNR(banknr) = 'Goed' THEN
									dbms_output.put_line('banknr');
									IF LENGTH(actievatiehash) = 32 THEN
										IF tussenvoegsel IS NULL THEN
											dbms_output.put_line('geentussenvoegsel');
											Insert into PERSOON ("voornaam","tussenvoegsel","achternaam","straat","huisnr","woonplaats","banknr") values (voornaam, null, achternaam, straat, huisnr, woonplaats, banknr);
											Insert into ACCOUNT ("gebruikersnaam","email","activatiehash","geactiveerd") values (gebruiker,email,actievatiehash,'0');
											errortext := 'goed';
											text := errortext;
										ELSE
											IF CHECKTEKST(tussenvoegsel) = 'Goed' THEN
												dbms_output.put_line('tussenvoegsel');
												Insert into PERSOON ("voornaam","tussenvoegsel","achternaam","straat","huisnr","woonplaats","banknr") values (voornaam, tussenvoegsel, achternaam, straat, huisnr, woonplaats, banknr);
												 
												errortext := 'goed';
												text := errortext;
											ELSE
												errortext := 'tussenvoegsel niet correct';
												text := errortext;
											END IF;
										
										END IF;
									ELSE
										errortext := 'activatiehash niet correct';
										text := errortext;
									END IF;
								ELSE
									errortext := 'banknummer niet correct';
									text := errortext;
								END IF;
							ELSE
								errortext := 'e-mail niet correct';
								text := errortext;
							END IF;
						ELSE
							errortext := 'gebruikernaam niet correct';
							text := errortext;
						END IF;
					ELSE
						errortext := 'huisnummer niet correct';	
						text := errortext;
					END IF;
				ELSE
					errortext := 'woonplaats niet correct';
					text := errortext;
				END IF;
			ELSE
				errortext := 'straat niet correct';
				text := errortext;
			END IF;
		ELSE
			errortext := 'achternaam niet correct';
			text := errortext;
		END IF;
	ELSE
		errortext := 'voornaam niet correct';
		text := errortext;
	END IF;
END;

/
CREATE OR REPLACE 
PROCEDURE subinschrijving 
(
email "ACCOUNT"."email"%TYPE,
gebruiker "ACCOUNT"."gebruikersnaam"%TYPE,
actievatiehash "ACCOUNT"."activatiehash"%TYPE,
text out VARCHAR2
)
AS
errortxt VARCHAR2(2000);
BEGIN
IF CHECKTEKST(gebruiker) = 'Goed' THEN
	dbms_output.put_line('gebruiker');	
	IF CHECKEMAIL(email) = 'Goed' THEN
		dbms_output.put_line('email');
		IF LENGTH(actievatiehash) = 32 THEN
			Insert into ACCOUNT ("gebruikersnaam","email","activatiehash","geactiveerd") values (gebruiker,email,actievatiehash,'0');
			errortxt :=  'account aangemaakt';
			text := errortxt;
		ELSE
			errortxt :=  'activatiehash niet correct';
			text := errortxt;
		END IF;
	ELSE
		errortxt :=  'email niet correct';
		text := errortxt;
	END IF;
ELSE
	errortxt := 'gebruikersnaam niet correct';
	text := errortxt;
END IF;
END;

