Earth Invaders
A játékról
Ez a játék Space Invaders mintájára készül. A játékos folyamatosan megy vízszintesen,
miközben ellenségeket hatástalanít. A játék során ellenségek kerülhetnek elé, annak
hatástalanításáért a játékos pontokat kap. A játék gyorsasága a megtett táv függvényében
gyorsul, ezáltal folyamatosan nehezedik.
A régi árkád gépek mintájára a játékos a legjobb n helyezettet elmenti egy listára. (High Score
lista)
A játékos a játék elején 5 élettel és 10 lövedékkel indul. A lövedék segítségével az ellenség
hatástalanítható. Ha a játékos ellenséggel ütközik, életének száma eggyel csökken. Lehetőség
van bónusz lövedékcsomagok és bónusz életek gyűjtésére a játék közben, így elméletben akár
a végtelenségig is játszható.
Látványtervek
Háttér
Játékos
Ellenség
Jutalmak
Lövedék
A játék működése
A program két projektből épül fel, az egyik a logikáért, a másik pedig a megjelenítésért felelős.
Logika
Logic.Models névtér
A Models névtér alapfeladata, hogy a játékban előforduló objektumokat (dinamikus, mozgó –
pl. játékos, ellenség, jutalmak), illetve a statikus (fal, levegő (amiben a játékos mozog))
definiálja.
Továbbá ebben a névtérben inicializáljuk a térképet a Map osztály segítségével
Ennek diagramja itt látható:
Ebben a névtérben található továbbá a Map osztály is, amelynek feladata, hogy a játék
objektumokat a Constants osztályban definiált szabályok szerint (gyakoriság, sebesség) a
térképen belül véletlenszerű magasságban, a látható térkép jobb szélére legenerálja, a játékos
irányába mozgassa, azok ütközését észlelje, és átadja GameLogic osztálynak. Ütközés esetén
a CheckDie metódus is lefut, mivel vannak ütközések, amelyek a játékos életét csökkentik,
vagy fallal ütközés esetén egyből nullázza. Ha a játékos meghalt, a játéknak vége van, ilyenkor
lehetőség van új játékot kezdeni, vagy kilépni a programból. Az osztály továbbá lehetővé teszi
az aktuális játékállapot mentését, ebben az esetben egy fájl felső sorába bekerül a térkép
dimenziója, alatta lévő sorokban az aktuális állapota (pl. ha adott koordinátán Enemy van,
akkor az adott karakter e lesz), illetve az aljára pedig, hogy a játékosnak a mentés pillanatában
hány pontja, tölténye, élete volt.
Logic névtér
Itt található a Constants osztály, ez egy egyszerű, ám a játék szempontjából annál fontosabb
része a programnak, ugyanis itt adtuk be, hogy indításkor hány élete, tölténye és pontja
(nyilvánvalóan ez 0) legyen a játékosnak, illetve milyen gyorsan mozogjanak a különböző
objektumok, a játéktér milyen gyorsan frissüljön.
A GameLogic osztály a program „lelke”, az IGameControl és IGameModel interfészeken
keresztül a megjelenítési réteg (UI) ezzel kommunikál. A mentett játékmenet visszatöltéséért
is ez az osztály a felelős, illetve ez az osztály indítja el, vagy állítja meg a játék időzítőt (pause
vagy a játékos halála esetén van szükség ezekre).
Megjelenítés
Controller névtér
Az itt található GameController osztály az IGameControl interfész segítségével a játék alapvető
tulajdonságait továbbadja a UI-nak, és elindítja a játékot. Az aktuális pontszám, life, ammo
kiírandó stringjét is ez az osztály készíti el, ami a MainWindowViewModel osztály által fog a
megjelenítésben frissülni. Továbbá a megnyomott gombnak megfelelően meghívja az
interfészen keresztül a GameLogic osztály megfelelő metódusait (fel-le mozgás, lövés,
betöltés, mentés, pillanatmegállítás, azonnali kilépés).
Renderer névtér
A Display osztály – nevéből fakadóan – a játék megjelenítéséért felelős. Hogy ne csússzon
össze túlságosan a játéktér, 50x50-es, vagy azalatti méretű ablakba nem rajzolunk bele (bár
alapértelmezés szerint egyébként is teljes képernyőn fut a program). Az OnRender metódus az
IGameModel interfész segítségével megkapja a Map osztálytól a kirajzolandó elemeket és
koordinátákat, és az adott elemnek megfelelő képet megjeleníti azokat az UI-on. A
megjelenítendő (háttér)képek, ikonok, illetve lejátszandó zenék (főmenü zene, játék zene)
mappája ebben a rétegben találhatók.
VM névtér
Az MWVM osztály azért felelős, hogy a játék közben a kiírt adatok (aktuális pontszám,
aktuális életek és töltények száma) folyamatosan frissítésre kerüljenek a játékos számára.
Ablakok
A játék két ablakból áll, az egyik az indításkor megjelenő menü, ahol a játék indítására, a játék
készítőinek megtekintésére, segítség megtekintésére (milyen billentyűleütéssel milyen
funkciót érhetünk el a játék közben) és a játékból kilépésre van lehetőségünk.
A főablak a játék közben megjelenő ablak, az ablak tetején az aktuális adatok (pontszám, életek
száma, töltények száma) láthatók, alatta pedig a játékteret jeleníti meg. Ha a játéknak vége
(akár escape-el befejezés, akár a játékos falhoz ütközése/életének nullára csökkenése miatt),
egy párbeszédpanel jelenik meg, ahol el lehet dönteni, hogy új játékot kezdünk-e, vagy
visszalépünk a menübe, ahonnan bezárhatjuk a programot.
