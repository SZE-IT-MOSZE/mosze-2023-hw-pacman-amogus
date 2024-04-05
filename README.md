# Modern szoftverfejlesztési eszközök (GKNB_INTM006) 2023/24 1.Félév

---

## Project Amogus

### A játék rövid leírása

A játékosnak egy labirintust kell bejárnia és különleges tárgyakat felvennie. A játékost ellenséges karakterek és csapdák is akadályozzák. A labirintus bejárása során a hibamentes bejárás és a különleges tárgyak felvétele folyamatosan pozitívan kerülnek elbírálásra, ugyanakkor az ellenségekkel és a csapdákkal való ütközés a játékosra nézve negatív következményekkel jár. A játék pályái véletlenszerűen fájlokba generálódnak, ahonnan a program később a játékmenet során betölti azokat.

### Játékelemek

- A játékos által kiválasztható véletlenszerűen generált labirintusok
- Ellenfelek amik a labirintus bejárását akadályozzák
- A játékos által összegyűjthetű különleges objektumok, amiknek begyűjtése pontokkal és egyéb előnyökkel jutalmazza a játékost.
- Három szabadon menthető és betölthető pálya.

## Történetleírás

1945, a Harmadik Birodalom területén Lengyelországban a nácik egyik titkos bázisában a Project Riese-ben vagyunk. A nácik látszólag sietve hagyták el ezt a bázist, senki sem tudja, hogy a közelgő vörös hadsereg miatt vagy lent találtak vagy alkottak valami rettenetest. Nagy felelősség hárult rám, mert én voltam az, aki belevágtam ebbe a kísérletbe, hogy megtudjam az igazságot. Korábbi információk alapján tudtam, hogy itt valami sötét és misztikus történt, legalábbis a titkosított dokumentumok említettek egy sikeresen megfejtett üzenetet, amiben németek az okkult kísérletek sikerességéről jelentettek. Amikor először találkoztam a náci szellemekkel, akiket itt hagytak, úgy éreztem, mintha az elmém is megbénult volna. A kísérteties lények csendben keringtek a folyosókon, a homályban néha eltűnve. Az egyetlen dolog, amit biztosan tudtam az volt, hogy mindenképpen el kell kerülnöm őket és össze kell gyűjteni bármit, amit csak találok. A Project Riese kiléte valószínűleg örökre rejtély marad, és csak a bátor kutatók és felfedezők próbálkoznak majd, hogy leleplezzék valódi célját.

---

## A játék működése

### Labirintusok generálása

Amennyiben még nincs generálva labirintus, a játékos a "Generate" gomb megnyomásával kerülhet a labirintus generátor menübe ahol már egy elkészült labirintus fogja fogadni. Amennyiben ez a labirintus nem felel meg a játékos igényeinek, a "Generate" gomb ismételt megnyomásával egy új labirintust fog kapni. Ha ez a pálya megfelel, a "Save" gomb megnyomásával a felhasználó ki tudja választani, hogy melyik mentési indexre szeretne menteni (vagy felűlírni a meglévő mentését.) Amennyiben a mentés megtörtént, akár ebből, akár a főmenűből a megfelelő mentési index gomb (1-2-3) megnyomásával betölthető az elmentett labirintus, és megkezdődhet a játék.

### Általános játékmenet

A játékos feladata, hogy a labirintus minden pontját bejárva, különös ereklyéket gyűjtsön össze. A karakter mozgatása a WASD gombokkal vagy a kurzormozgató billentyűkkel történik. Az ereklyék összegyűjtése pontokkal jutalmazza a játékost, ám az ellenségekkel és csapdákkal történő ütközés ellenben pont és életveszteséggel történik. Amennyiben a játékos összes élete elfogy, a játék véget ér.

---

## Technikai Specifikációk

### A játék által kezelt könyvtár

A program a játék pályáinak betöltéséhez XML-típusú adat szerializálást használ. A program a generált labirintusokat egy .save kiterjesztésű fájlba menti.

A mentéseket tároló mappának az elérési útját az Application.PersistentDataPath() method adja meg, ami a felhasználó által a ==C:\Users\userXY\AppData\LocalLow\Amogus== elérési úton található meg.

### Tesztesetek

A megírt tesztesetek az ==Assets\Tests\Edit Tests== és ==\Play Tests== mappákban találhatóak meg.

Futtathatásukhoz 2023.3.10f verziószámú Unity Editor szükséges.

---

## Csapattagok

- *Poczok Ádám*
- *Vati Albert*
- *Boda Péter*
- *Pósza Levente*
  
- *Külön köszönettel jó barátunknak, István Kándárli-nak a háttérzeneként felhasznált dal felénekléséért!*
