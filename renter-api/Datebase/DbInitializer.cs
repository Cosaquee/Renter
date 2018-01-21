using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Utils;
using System;
using System.Threading.Tasks;

namespace Database
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DbContext dbContext;

        public DbInitializer(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<Role> roles;
        private DbSet<User> users;
        private DbSet<Category> categories;
        private DbSet<Director> directors;
        private DbSet<Movie> movies;
        private DbSet<Author> authors;
        private DbSet<Book> books;

        public async Task Seed()
        {
            roles = dbContext.Set<Role>();
            users = dbContext.Set<User>();
            categories = dbContext.Set<Category>();
            directors = dbContext.Set<Director>();
            movies = dbContext.Set<Movie>();
            authors = dbContext.Set<Author>();
            books = dbContext.Set<Book>();
            authors = dbContext.Set<Author>();
            books = dbContext.Set<Book>();

            await SeedRoles();
            await SeedUsers();
            await SeedCategories();
            await SeedDirectors();
            await SeedMovies();
            await SeedAuthors();
            await SeedBooks();
        }

        private async Task SeedRoles()
        {
            if (roles == null || await roles.AnyAsync())
            {
                return;
            }

            roles.Add(new Role { Name = "User" });
            roles.Add(new Role { Name = "Employee" });
            roles.Add(new Role { Name = "Administrator" });

            dbContext.SaveChanges();
        }

        private async Task SeedUsers()
        {
            if (users == null || await users.AnyAsync())
            {
                return;
            }

            var userRole = await roles.FirstOrDefaultAsync(x => x.Name == "User");
            var moderatorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Employee");
            var administratorRole = await roles.FirstOrDefaultAsync(x => x.Name == "Administrator");

            users.Add(new User
            {
                Role = userRole,
                UserName = "testUser",
                Password = PasswordHasher.CalculateHashedPassword("testUser"),
                Email = "testUser@tt.tt",
                Name = "Test",
                Surname = "User"
            });

            users.Add(new User
            {
                Role = moderatorRole,
                UserName = "testEmployee",
                Password = PasswordHasher.CalculateHashedPassword("testEmployee"),
                Email = "testEmployee@tt.tt",
                Name = "Test",
                Surname = "Moderator"
            });

            users.Add(new User
            {
                Role = administratorRole,
                UserName = "testAdministrator",
                Password = PasswordHasher.CalculateHashedPassword("testAdministrator"),
                Email = "testAdministrator@tt.tt",
                Name = "Test",
                Surname = "Administrator"
            });


            users.Add(new User
            {
                Role = administratorRole,
                UserName = "cosaquee",
                Password = PasswordHasher.CalculateHashedPassword("password"),
                Email = "cosaquee@gmail.com",
                Name = "Karol",
                Surname = "Kozakowski",
                ProvileAvatar = "https://avatars2.githubusercontent.com/u/10079912?s=460&v=4"
            });

            users.Add(new User
            {
                Role =  moderatorRole,
                UserName = "jarzabek",
                Password = PasswordHasher.CalculateHashedPassword("password"),
                Email = "jarzabek@gmail.com",
                Name = "Piotr",
                Surname = "Jarzabek",
                ProvileAvatar = "https://avatars3.githubusercontent.com/u/16638214?s=460&v=4"
            });

            dbContext.SaveChanges();
        }

        private async Task SeedAuthors()
        {
            if (authors == null || await authors.AnyAsync())
            {
                return;
            }

            authors.Add(new Author { Name = "Paweł", Surname = "Kornew", Description = "Urodził się w Czelabińsku w 1978 roku. \n W 2000 roku ukończył ekonomiczny fakultet Czelabińskiego Uniwersytetu Narodowego, obecnie pracuje w swojej specjalizacji. \n W miarę regularnie zaczął pisać od 2003 roku, ze swoimi opowiadaniami brał udział w wielu internetowych konkursach, lecz bez szczególnego sukcesu. \n Zainteresowania/pasje: literatura fantasy i fantastyka. \n Ulubieni zagraniczni autorzy: P. Żelazny, J. Martin, S. Grin. \n Ulubione ksiażki: z rodzimch pewnie pierwsze dwa Swaroga Byszkowa, \"Kroniki Sally\" Pechowa, \"Doroga domoj\" Zykowa i \"Tajne miasto\" Panowa. I jeszcze wielu autorów, których nie wymienię. \n Muzyka: \"Piknik, \"Nautilus Pompilius\", \"Fort Royal\", \"Czarny obelisk\", wczesne albumy \"Korolja i Szuta\" i \"Agaty Kristi\".\n Gry kumputerowe: DPG i TBS ( podobnie jak wcześniej, tyle że ostatnio brakuje na to czasu). \n Używki: ciemne piwo i koniak." });
            authors.Add(new Author { Name = "Bartek", Surname = "Biedrzycki", Description = "Rocznik ’78, mąż Ali, ojciec Zuzy i Bruna, zamieszkuje w aglomeracji warszawskiej. Przez ponad dekadę pracował przy produkcji telewizyjnej, obecnie jest specjalistą IT. \n Ma niepotrzebny dyplom UW, uprawniający go do pracy w charakterze nauczyciela j. angielskiego. \n Starszy kapral rezerwy, po wzorowej służbie w Dywizjonie Artylerii Samobieżnej w Orzyszu. W armii zdarł dwie pary butów, za to wyniósł stamtąd honorowe krwiodawstwo. \n Fan komiksu od zawsze, zgodnie z opowieściami na komiksie nauczył się czytać. Odkąd miał palce na tyle sprawne, aby utrzymać kredkę – rysował po czym się dało. Pierwszą komiksową produkcją była ośmiostronicowa ilustracja do odcinka Pszczółki Mai, wykonana w wieku mniej więcej 4 lat. \n Napisał dużo tekstów o komiksach i kilka o załogowym podboju kosmosu. W wolnych chwilach skleja i projektuje modele papierowe z tej drugiej dziedziny." });
            authors.Add(new Author { Name = "Michał", Surname = "Gołkowski", Description = "Zodiakalnie, patologiczny Wodnik z roku Stanu Wojennego. Wbrew pozorom samotnik, mizantrop i introwertyk. \n Z wykształcenia lingwista, z zamiłowania historyk wojskowości, z zawodu na co dzień tłumacz kabinowy ang-pol-ros. \n Obecnie stalker. \n Czarnobylem zafascynowany, odkąd tylko dowiedział się, po co brał wiosną ’86 ten niedobry proszek w kapsułkach z opłatka. \n Swoje związki ze Wschodem i stosunek do słowiańskości określa jako typowy love-hate relationship. \n Od zawsze rozerwany pomiędzy Skansenem w Łowiczu a Muzeum Wojska Polskiego w Warszawie, osiadł w końcu pod lasem niedaleko Sochaczewa." });
            authors.Add(new Author { Name = "Jarosław", Surname = "Grzędewicz", Description = "Urodzony w 1965 roku, debiutował w 1982 na łamach tygodnika „Odgłosy” opowiadaniem \"Azyl dla starych pilotów\". Opublikowane w 1999 roku w Internecie opowiadanie \"Klub absolutnej karty kredytowej\" otrzymało nominację do Nagrody Elektrybałta, a po publikacji w \"Wizjach alternatywnych\" 2002 także nominację do Nagrody Sfinksa. W 1990 roku wraz z Andrzejem Łaskim, Krzysztofem Sokołowskim, Dariuszem Zientalakiem i Rafałem Ziemkiewiczem założył magazyn literacki „Fenix”, w którym prowadził dział prozy polskiej, a od 1993 roku był jego redaktorem naczelnym. Pracuje jako dziennikarz, prowadzi stałą rubrykę naukowo-cywilizacyjną w \"Gazecie Polskiej\" i tłumaczy komiksy. \n Laureat Śląkfy w kategorii Twórca Roku 2005. W 2006r. pisarza uhonorowano również Sfinksem, w kategorii Polska Powieść Roku za książkę \"Pan Lodowego Ogrodu, t.1\". \nJest pierwszym w historii literackiej Nagrody im. Janusza A. Zajdla zdobywcą dwóch statuetek w obu konkursowych kategoriach: Powieść oraz Opowiadanie roku 2005. Nagrodzona powieść to \"Pan Lodowego Ogrodu, t.1\", opowiadaniem jest \"Wilcza zamieć\" ze zbioru \"Deszcze niespokojne\". W roku 2007 uhonorowany został kolejnym Zajdlem - powieścią roku 2006 uznano \"Popiół i kurz. Opowieść ze świata Pomiędzy\"." });
            authors.Add(new Author { Name = "Andrzej", Surname = "Pilipiuk", Description = "Andrzej Pilipiuk (1974) człowiek z przeszłości. Niestrudzony tropiciel ciekawostek z lamusa. Kolekcjoner nagród literackich, który z pisania z pasją uczynił swój sposób na życie. Miarą jego sukcesu jest 26 napisanych powieści wydanych w ciągu dekady, 600 tysięcy sprzedanych książek i miejsce na podium ścisłej czołówki najpoczytniejszych pisarzy w Polsce. \n Homo literatus, który do pisania podchodzi z żelazną regułą – pracuje planowo, codziennie, a kiedy poczuje zmęczenie fabułą, zabiera się za inny tytuł. Uprzedzając krytykę sam siebie nazwał Wielkim Grafomanem. Z wykształcenia archeolog, z zamiłowania łowca meteorów. Beznadziejnie zauroczony zapomnianymi odkrywcami i wynalazkami XIX wieku. Społecznik. Własnym sumptem i ogromnym zaangażowaniem wydał unikatowy album o Wojsławicach, mieście w którym narodził się Jakub Wędrowycz" });
            authors.Add(new Author { Name = "Andrzej", Surname = "Ziemiański", Description = "Porzucił prestiżową karierę naukową i zajął się pisaniem książek. Już po kilku latach zdobył uznanie pół miliona Polaków czytających Achaję z wypiekami na twarzach. \n Z wykształcenia jest architektem. Widzi i myśli obrazami. Podgląda, słucha i rozmawia dla przyjemności. Lubi fotografować puste miejsca. Sam jest dla siebie pracownikiem, szefem i przewodniczącym komitetu strajkowego. Nieustannie prowokuje czytelników i recenzentów swoim podejściem do sztuki pisania. Mówi, że cierpi na przymus tworzenia. \n Jego bohaterowie żyją własnym życiem – sami wybierają gatunek i formę opowieści, dzięki czemu z niezrozumiałego dla siebie powodu autor otrzymał wór nagród od czytelników. Pierwsze dwa tomy „Achai” nominowane były do Nagrody im. Janusza A. Zajdla. Drugi tom otrzymał w 2004 roku Nagrodę Nautilus." });
            authors.Add(new Author { Name = "Jakub", Surname = "Ćwiek", Description = "Polski pisarz, miłośnik i znawca popkultury oraz muzyki rockowej. Studiował kulturoznawstwo na Uniwersytecie Śląskim w Katowicach. Prywatnie ojciec dwójki dzieci – Zuzi i Samuela." });
            authors.Add(new Author { Name = "Dan", Surname = "Brown", Description = "Amerykański pisarz, autor powieści sensacyjnych. \n Jest absolwentem Phillips Exeter Academy, ekskluzywnej szkoły średniej z internatem, gdzie jego ojciec był nauczycielem matematyki, potem ukończył Amherst College (1986). Zanim rozpoczął karierę pisarską, zajmował się muzyką, wydał dwa nagrania, jednak nie okazały się one sukcesem. Po zaprzestaniu kariery muzycznej uczył języka angielskiego w swojej byłej szkole, Phillips Exeter. Obecnie mieszka w Nowej Anglii. Jego żona Blythe jest z zawodu ilustratorką, pasjonuje się historią sztuki i malarstwem, współpracuje z Brownem przy tworzeniu powieści." });
            authors.Add(new Author { Name = "Tomasz", Surname = "Pacyński", Description = "(ur. 4 lutego 1958 w Warszawie, zm. 30 maja 2005 w Broku) – polski pisarz fantastyczny, z zawodu informatyk. Redaktor naczelny czasopisma internetowego Fahrenheit. Publikował na łamach Science Fiction, SFery i Fantasy oraz w internetowych czasopismach Fahrenheit, Esensja, Fantazin i Srebrny Glob. Autor felietonów publikowanych w SFerze i Science Fiction. Jego debiutem książkowym była opublikowana w 2001 powieść Sherwood, za którą otrzymał nominację do nagrody im. Janusza A. Zajdla. Opublikował cztery powieści – trylogię fantasy Sherwood (na motywach legendy o Robin Hoodzie) oraz Wrzesień – militarną postapokaliptyczną powieść political fiction, również nominowaną do Zajdla, a także liczne opowiadania. Humorystyczne opowiadania z cyklu o Dziadku Mrozie zostały w 2005 wydane w zbiorze Linia ognia. W Internecie używał często pseudonimu Pacek."});
            authors.Add(new Author { Name = "Eugeniusz", Surname = "Dębski", Description = "Polski pisarz fantastyczny, członek Stowarzyszenia Pisarzy Polskich (incydentalnie publikujący również pod pseudonimami Owen Yeates, Pierce O'Otooley i innymi), od 1997 członek Stowarzyszenia Pisarzy Polskich, od 2005 roku prezes SPP oddział Wrocław. W 1976 ukończył rusycystykę na Uniwersytecie Wrocławskim obecnie mieszka na stałe we Wrocławiu. Jako autor science fiction debiutował w 1984 roku opowiadaniem Najważniejszy dzień 111394 roku. Pod tym samym tytułem w 1985 ukazał się jego pierwszy zbiór opowiadań. Autor kilkunastu powieści oraz kilkudziesięciu opowiadań science fiction i fantasy, z których kilka przetłumaczonych zostało na niemiecki, węgierski, czeski i rosyjski. Sześć powieści z cyklu o przygodach Owena Yeatesa wydano już w Rosji, pozostałe w tłumaczeniu, w Rosji też opublikowany niedługo będzie przekład jednej z pierwszych polskich powieści fantasy Śmierć Magów z Yara, wcześniej wydanej już w Czechach." });
            authors.Add(new Author { Name = "Michael", Surname = "Dobbs", Description = "Członek brytyjskiej Partii Konserwatywnej i pisarz." });
            authors.Add(new Author { Name = "Tom", Surname = "Clancy", Description = "Jeden z najlepszych i najbardziej znanych na świecie twórców literatury sensacyjnej. Karierę zaczynał jako niezależny biznesmen, później poświęcił się wyłącznie pisarstwu. Nie zerwał jednak do końca kontaktu z biznesem - miał własną fabrykę tworzącą gry komputerowe, których był pasjonatem. Rozpoczął swoją karierę w 1984 roku, publikując Polowanie na Czerwony Październik, które pobiło wszelkie rekordy popularności. Wszystkie jego kolejne powieści niezmiennie zajmowały pierwsze miejsca na listach bestsellerów. Powstało też kilka filmów na podstawie jego książek. Twórczość Toma Clancy'ego cechuje głęboka znajomość realiów współczesnej geopolityki, kulis działania agend rządowych i sił zbrojnych: od szczebla plutonu piechoty do broni kosmicznej. Rdzeń zaś stanowią książki opisujące dzieje Jacka Ryana - gracza giełdowego, historyka, oficera piechoty morskiej, wysokiego urzędnika Centralnej Agencji Wywiadowczej i... prezydenta Stanów Zjednoczonych Ameryki. Tom Clancy był również (wspólnie ze Steve'em Pieczenikiem) autorem czterotomowego cyklu powieściowego CENTRUM, którego pierwsza część doczekała się ekranizacji. Zmarł 2 października 2013 w szpitalu w Baltimore." }); 
            authors.Add(new Author { Name = "David", Surname = "Morrell", Description = "Kanadyjski/amerykański pisarz. \n W 1966 roku przeprowadził się do Stanów Zjednoczonych. Był wykładowcą uniwersyteckim do 1986 roku. W Polsce wydano w latach 1990-2005 ponad dwadzieścia jego książek. \n Najbardziej znany tytuł to Rambo: Pierwsza krew, podstawa cyklu filmów z Sylvestrem Stallone w roli głównej (First Blood, 1972)." }); 
            authors.Add(new Author { Name = "Frederic", Surname = "Forsyth", Description = "Urodzony 25 sierpnia 1938 roku w Ashford, hrabstwo Kent w Anglii w bogatej, mieszczańskiej rodzinie. Uczył się w Tonbridge. Następnie dostał się na Uniwersytet Granady w Hiszpanii, ale ledwie rozpoczęte studia przerwał, by poświęcić się swojej pasji - lataniu. Gdy miał 17 lat, uzyskał licencję pilota, a w wieku lat 19 został jednym z najmłodszych pilotów w historii RAF (Royal Air Force - Królewskie Siły Powietrzne), gdzie służył w latach 1956-1958."});
            
            dbContext.SaveChanges();
        }

        private async Task SeedCategories()
        {
            if (categories == null || await categories.AnyAsync())
            {
                return;
            }
            categories.Add(new Category { Name = "Dramat" });
            categories.Add(new Category { Name = "Biograficzny" });
            categories.Add(new Category { Name = "Horror" });
            categories.Add(new Category { Name = "Thriller" });
            categories.Add(new Category { Name = "Sci-fi" });
            categories.Add(new Category { Name = "Fantasy" });
            categories.Add(new Category { Name = "Akcja"});

            dbContext.SaveChanges();
        }

        private async Task SeedBooks()
        {
            if (books == null || await books.AnyAsync())
            {
                return;
            }
            var fantasy = await categories.FirstOrDefaultAsync(x => x.Name == "Fantasy");
            var scifi = await categories.FirstOrDefaultAsync(x => x.Name == "Sci-fi");
            var thriller = await categories.FirstOrDefaultAsync(x => x.Name == "Thriller");
            var akcja = await categories.FirstOrDefaultAsync(x => x.Name == "Akcja");

            var bartekBiedrzycki = await authors.FirstOrDefaultAsync(x => x.Name == "Bartek");
            var danBrown = await authors.FirstOrDefaultAsync(x => x.Name == "Dan");
            var michal = await authors.FirstOrDefaultAsync(x => x.Name == "Michał");
            var tomasz = await authors.FirstOrDefaultAsync(x => x.Name == "Tomasz");
            var pilipiuk = await authors.FirstOrDefaultAsync(x => x.Name == "Andrzej" && x.Surname == "Pilipiuk");
            var pawelKornew = await authors.FirstOrDefaultAsync(x => x.Name == "Paweł");
            var eugeniusz = await authors.FirstOrDefaultAsync(x => x.Name == "Eugeniusz");
            var dobbs = await authors.FirstOrDefaultAsync(x => x.Name == "Michael");
            var clancy = await authors.FirstOrDefaultAsync(x => x.Name == "Tom");
            var morrell = await authors.FirstOrDefaultAsync(x => x.Name == "David");
            var forsyth = await authors.FirstOrDefaultAsync(x => x.Name == "Frederic");

            books.Add(new Book { Title = "Lodowa Cytadela", ISBN = "9788379640935", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "Przygranicze to parę miast kilkadziesiąt lat temu wyrwanych z naszego świata i przeniesionych do krainy wiecznego mrozu. Dziwne miejsce, w którym przez większą część roku panują chłody i wieją lodowate wiatry, a ludzie nabywają magicznych zdolności, co czyni ich bardziej niebezpiecznymi od uzbrojonych po zęby bojowników. Lecz człowiek umie przystosować się do każdych warunków, więc Jewgienij Apostoł już dawno przestał żałować, że trafił do Fortu - dawnego prowincjonalnego miasteczka, które stało się centrum cywilizacji na tej pokrytej śniegiem ziemi. I nawet kiedy przyszło wziąć nogi za pas, aby ratować skórę przed najemnymi zabójcami, przede wszystkim pomyślał o tym, jak nie zmarnować szansy na życiowy interes.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/lodowa-cytadela.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/lodowa-cytadela.jpg" });
            books.Add(new Book { Title = "Czarne Południe", ISBN = "9788375747164", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "Przy Soplu twardziele topnieją \n Jeżeli nie zabije cię kula, to dobije cię czar. Nie osmali cię ogień, to skapiejesz na mrozie. Oderwanym skrawkiem naszego świata wstrząsa brutalna wojna. Stary i kruchy porządek ostatecznie się zawalił - czas wybrać nowych władców. Ręka, która utrzyma nóż, rządzić będzie Przygraniczem. \n I dlatego wszyscy, którzy chcą się liczyć w nowym układzie sił szukają Sopla. \n Sopel, by przeżyć i wyrwać się z Przygranicza musi szukać noża. \n Napalm, jak na piromantę przystało, szuka okazji do rozróby. \n Zanim nastanie Czarne Południe wszystkim uczestnikom te poszukiwania bokiem wylezą. Niektórym razem z flakami.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/czarne-poludnie.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/pawel-kornew-czarne-poludnie.jpg" });
            books.Add(new Book { Title = "Sopel", ISBN = "9788379642267", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "W świat Przygranicza trafiasz przypadkiem. Z woli losu. Wysiadasz z wagonu, by na postoju napić się piwa. Chwilę potem nie ma ani stacji, ani pociągu, ani torów, ani dworcowej knajpy, z której właśnie wyszedłeś. \n Tylko mróz, śnieg, zamieć. I konieczność toczenia ciągłych walk. Z ludźmi i innymi bestiami. \n Światem Przygranicza rządzi zasada „wszyscy przeciwko wszystkim”.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/sopel.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/sopel.JPG" });
            books.Add(new Book { Title = "Sopel", ISBN = "9788379642267", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "W świat Przygranicza trafiasz przypadkiem. Z woli losu. Wysiadasz z wagonu, by na postoju napić się piwa. Chwilę potem nie ma ani stacji, ani pociągu, ani torów, ani dworcowej knajpy, z której właśnie wyszedłeś. \n Tylko mróz, śnieg, zamieć. I konieczność toczenia ciągłych walk. Z ludźmi i innymi bestiami. \n Światem Przygranicza rządzi zasada „wszyscy przeciwko wszystkim”.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/sopel.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/sopel.JPG" });
            books.Add(new Book { Title = "Dworzec Śródmieście", ISBN = "9788379641825", AuthorId = bartekBiedrzycki.Id, CategoryId = scifi.Id, Description = "Warszawa po Zagładzie to o wiele więcej, niż metro. Ludzie żyją tu na podziemnych dworcach, w piwnicach, w zapomnianych przejściach. Mieszkańcy tych miejsc śmieją się i płaczą, kochają i nienawidzą, czekają – czasem sami nie wiedzą na co; szukają – rzadko wiedząc czego. Żyją i umierają. Ale czasem, kiedy umierają zbyt gwałtownie, krucha równowaga i prowizoryczna umowa społeczna zostają naruszone. Czasem za krew można zapłacić nabojami, czasem książkami, a czasem odpłatą może być tylko krew.  Czy po wojnie atomowej nie ma już dobrych ludzi? Czy każdy człowiek, nawet ten z pozoru najlepszy, ma w sobie utajonego zbrodniarza, który za godziwą zapłatę zrobi wszystko? Ile warte jest w takim świecie życie – życie brata, życie męża, życie syna... Napisana z wielkim rozmachem kulminacja przygód rudobrodego stalkera Borki z Kryształowego Pałacu, pokazująca życie Warszawy poza systemem stołecznego metra od chwili Zagłady, aż po czasy najnowsze. Klucz do zrozumienia wydarzeń trylogii warszawskiej, odkrywający wszystkie tajemnice i misterną siatkę powiązań między pozornie nieznajomymi bohaterami.", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/bartek-biedrzycki-dworzec-srodmiescie.JPG", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/bartek-biedrzycki-dworzec-srodmiescie.JPG" });
            books.Add(new Book { Title = "Początek", ISBN = "9788381101431", AuthorId = danBrown.Id, CategoryId = thriller.Id, Description = "Robert Langdon, profesor Uniwesytetu Harvarda i specjalista od symboli, przenosi nad do malowniczej Hiszpanii. Tam przemierza ulice Madrytu, Barcelony, Sewilli i Builbao w poszukiwaniu odpowiedzi na nurtujące ludzkość pytania: \n Skąd pochodzimy? \n Dokąd zmierzamy? \n Brown zapełnił karty tej fascynującej powiesci kodami, symbolami, odkryciami naukowymi, ciekawostkami religijnymi, historycznymi i architektonicznymi. Powiewu świezości dodają opisy dzieł sztuki współczesnej i najnowocześniejszych rozwiązań technologicznych! \n Bez względu a to, kim jesteś i w co wierzysz, nie masz wpływu na to, co się wydarzy...", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/początek.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/początek.jpg"});
            books.Add(new Book { Title = "2586 kroków", ISBN = "9788375749298", AuthorId = pilipiuk.Id, CategoryId = fantasy.Id, Description = "I znowu odliczamy 2586 kroków... Tylko tyle. I aż tyle. Opowiadanie po opowiadaniu. To już V wydanie antologii, w nowej okładce, bez Wędrowycza, za to ze znakiem firmowym Pilipiuka w postaci niepowtarzalnego humoru i zdumiewających pomysłów. Czternaście historii, z których wieje grozą i mrocznym nastrojem. Odliczmy więc 2586 kroków...", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/2568-krokow.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/2568-krokow.jpg"});
            books.Add(new Book { Title = "Szatański Interes", ISBN = "83-89011-75-1", CategoryId = fantasy.Id, AuthorId = tomasz.Id, Description = "Szatański interes, zbiór fabularnie powiązanych opowiadań kontynuuje przygody pary bohaterów Linii ognia zwariowanej dziewczyny, która kazała nazywać się Matyldą, i Pawła Morozowa, tak naprawdę Dziadka Mroza. W świecie, który okazał się okrutniejszy, niż się wydawało, muszą sprzymierzyć się z silniejszymi od siebie. I uczyć, choćby od diabła. Głęboko ukryta, w tle globalnych konfliktów, toczy się walka wywiadów. Lecz i to są tylko pozory, prawdziwy konflikt ukrywa się jeszcze głębiej. Matylda i Dziadek stają naprzeciw mrocznych baśni, starszych od rodzaju ludzkiego, grających według własnych reguł. Ale nie są już sami. Szatański interes to urban fantasy w realiach technologii militarnej dwudziestego pierwszego wieku. Świat, w którym przeciw zaklęciom i bestiom staje siła ognia. I para bohaterów, każde z nich musi zmierzyć nie tylko z wrogami, ale i z samym sobą.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/sztanski-interes.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/sztanski-interes.jpg"}); 
            books.Add(new Book { Title = "Komornik", ISBN = "9788379641338", CategoryId = fantasy.Id, AuthorId = michal.Id, Description = "Nadchodzi Koniec. Ale taki w cholerę prawdziwy, biblijny. Ziemia zatrzymuje się, gwiazdy spadają, woda zamienia się w krew. Umarli wstają z grobów, otwiera się otchłań. Państwa upadają, brat powstaje przeciw bratu, a dzieci podnoszą rękę na rodziców. Widać, że lada chwila świat spłonie. Tylko że coś w systemie nie zaskoczyło. Generalnie, zasadniczo i pobieżnie: zamiast bomby termojądrowej wychodzi fajerwerk.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/komornik.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/komornik.jpg"});
            books.Add(new Book { Title = "Komornik - Kant", ISBN = "9788379642830", CategoryId = fantasy.Id, AuthorId = michal.Id, Description = "Wypełniło się Proroctwo. Teraz pozostaje tylko jeden, ostatni akord. \n Oto nadchodzi dzień Pana, okrutny, pełen srogości i płonącego gniewu, aby obrócić ziemię w pustynię, a grzeszników z niej wytępić. \n Księga Izajasza 13:9, Psalm 86, Dzieje Apostolskie 9 \n Świat musi spłonąć. \n Ale jeszcze można pokazać pazur. Jeszcze można przetrącić skrzydełka tym zasranym cherubinkom. Jeszcze skundlona ludzkość do końca nie sczezła. \n Jak by tak zebrać wszystkich, nawet największych wypierdków, szaleńców, emerytowanych gladiatorów i najmroczniejsze bestie rewersu, to może uda się zasadzić skrzydlatym siarczystego kopa i zgasić świętoszkowate uśmieszki na gładkich gębach. \n Do boju ludzie, a ci z góry mogą nam naskoczyć na kant! ", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/komornik-kant.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/komornik-kant.jpg"});
            books.Add(new Book { Title = "Komornik - Rewers", ISBN = "9788379642335", CategoryId = fantasy.Id, AuthorId = michal.Id, Description = "Tam, gdzie nie sięga światło Łaski Pańskiej, rozciągają się jałowe pustkowia spowitego w wieczną, zimną noc Rewersu. \n Zatrzymana w pół obrotu Ziemia zastygła w terminalnym pacie pomiędzy siłami Dobra i Zła. \nWraz z Ezekielem Siódmym odważ się wejść tam, gdzie obawiają się stąpać nawet Aniołowie. Znajdź w sobie siłę, by spojrzeć w twarz temu, co od zawsze czaiło się pod twoim łóżkiem. Apokalipsa, owszem, nie udała się – co do tego nikt nie ma wątpliwości. Teraz okazuje się, jak kolosalne były rozmiary zaniedbań.", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/komornik-rewers.jpg", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/komornik-rewers.jpg"});
            books.Add(new Book { Title = "Russian Impossible", ISBN = "9788389595805", CategoryId = scifi.Id, AuthorId = eugeniusz.Id, Description = "To już nie rutynowa akcja. W Sankt Petersburgu trwają bezpardonowe i okrutne ataki na milicję. Zamachowcy ostrzeliwują z karabinów maszynowych samochody patrolowe. Płoną posterunki, obrzucone koktajlami Mołotowa. A na dodatek ktoś porwał córki kapitana Sukonina, który kieruje polsko-rosyjskim zespołem śledczym. \n Zdesperowany kapitan i jego partner, Kamil „Moherfucker” Stochard, spotykają się z Niebieskim Krukiem, carem przestępczego świata. \n Ale czy ta decyzja się opłaci? \n Czy cena za jego pomoc nie będzie zbyt wysoka? \n Czy kapitan zobaczy jeszcze swoje ukochane córki? \n Bohaterowie mają przeciwko sobie szczyty rosyjskich władz, świeckich i cerkiewnych. A także Cthulu oraz jego magiczny pomiot — guimony.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/russian-impossible.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/russian-impossible.jpg" });
            books.Add(new Book { Title = "House of Cards", ISBN = "9788324026555", CategoryId = thriller.Id, AuthorId = dobbs.Id, Description = "House of Cards – kto rozdaje karty na najwyższych szczeblach władzy? \n Opowieść-fenomen, która zachwyciła miliony widzów i czytelników na całym świecie. \n Człowieka motywuje nie szacunek, lecz strach. To na nim buduje się imperia i za jego sprawą wszczyna rewolucje. W tym tkwi sekret wielkich ludzi. Kiedy ktoś się ciebie boi, zniszczysz go, zmiażdżysz, a w efekcie on zawsze obdarzy cię szacunkiem. Prymitywny strach jest upajający, wszechogarniający, wyzwalający. Zawsze silniejszy od szacunku. Zawsze.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/michael-dobbs-house-of-cards.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/michael-dobbs-house-of-cards.jpg"});
            books.Add(new Book { Title = "Zwierzchnik", ISBN = "9788328009981", CategoryId = akcja.Id, AuthorId = clancy.Id, Description = "Dziewiąta książka z cyklu o Jacku Ryanie, byłym agencie CIA, prezydencie USA, opublikowana już po śmierci autora. \n Tajemnicza historia z czasów zimnej wojny i współczesne wydarzenia: Rosja atakuje Ukrainę, świat staje na krawędzi wielkiego konfliktu. Okoliczności szybkiego i niespodziewanego dojścia do władzy nowego prezydenta Rosji owiane są tajemnicą. Terror i zastraszanie to metody powstrzymywania wrogów przed poznaniem prawdy. Okazuje się, że jest jedna osoba, która może znać mroczny sekret rosyjskiego magnata – obecny prezydent Stanów Zjednoczonych, były agent CIA Jack Ryan. \n Numer 1 na liście bestsellerów „New York Timesa”!", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/tom-clancy-zwierzchnik.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/tom-clancy-zwierzchnik.jpg" }); 
            books.Add(new Book { Title = "Nagie ostrze", ISBN = "9788376596259", CategoryId = akcja.Id, AuthorId = morrell.Id, Description = "Aaron Cavanaugh, niegdyś komandos Delta Force, porzuca zawód ochroniarza i rozpoczyna spokojne życie farmera na kupionej za ostatnie oszczędności posiadłości w Wyoming. Niestety spokój nie potrwa długo. Pół roku później Aaron otrzymuje nieoczekiwany spadek - jego były szef zapisuje mu w testamencie swą agencję ochrony o międzynarodowym zasięgu, Global Protection Services. Wydarzenie to zbiega się w czasie z nagłym atakiem grupy zawodowych zabójców na farmę Aarona. On i jego żona Jamie, po gęstej wymianie ognia cudem uchodzą z życiem. To dopiero początek - ktoś nadal próbuje go zabić.Kto? - Cavanaugh podejrzewa, że seria zamachów ma związek ze zleceniami, jakie w przeszłości wykonywał jako agent ochrony. Kolejne zabójstwa dowodzą jednak, że sprawa ma szerszy zasięg. Masowo giną inni agenci, zarówno z GPS, jak i z agencji federalnych - ludzie, z którymi Aaron nie ma nic wspólnego.Mordowane są takze ich rodziny. Coś w sposobie działania zabójców sprawia, że Cavanaugh zyskuje pewność co do organizatora zamachów. Tylko jedna osoba pasuje do schematu: jego dawny przyjaciel i kumpel z Delta Force, fanatyk nozy. Ale jaki ma motyw?", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/david-morrell-nagie-ostrze.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/david-morrell-nagie-ostrze.jpg" });
            books.Add(new Book { Title = "Kobra", ISBN = "9788376592053", CategoryId = akcja.Id, AuthorId = forsyth.Id, Description = "Prezydent USA postanawia zniszczyć handlarzy kokainą. Zadanie zleca emerytowanemu agentowi CIA zwanemu Kobrą. Po długich przygotowaniach zaczyna akcję przejmowania transportów kokainy, likwidacji przekupionych celników na usługach kartelu. Przechwytywane statki i samoloty są zatapiane i giną bez śladu, a ich załogi lądują w tajnym więzieniu na wyspie. Szef kartelu orientuje się z czasem, że ma wroga, który może zniszczyć jego imperium. Na domiar złego ma kłopoty ze strony odbiorców narkowyków. Przechodzi do kontrataku i wybucha regularna wojna", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/frederyk-forsyth-kobra.jpg", ResizedCoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/resized/frederyk-forsyth-kobra.jpg<tab>" });
            
            dbContext.SaveChanges();
        }

        private async Task SeedDirectors()
        {
            if (directors == null || await directors.AnyAsync())
            {
                return;
            }

            directors.Add(new Director
            {
                Name = "Alexandre",
                Surname = "Bustillo"
            });

            directors.Add(new Director
            {
                Name = "Ethan",
                Surname = "Warren"
            });

            dbContext.SaveChanges();
        }

        private async Task SeedMovies()
        {
            if (movies == null || await movies.AnyAsync() || !(await categories.AnyAsync()) || !(await directors.AnyAsync()))
            {
                return;
            }

            var horror = await categories.FirstOrDefaultAsync(x => x.Name == "Horror");
            var drama = await categories.FirstOrDefaultAsync(x => x.Name == "Dramat");

            movies.Add(new Movie
            {
                Category = horror,
                Title = "Leatherface",
                DirectorId = 1,
                Duration = new TimeSpan(1, 35, 0),
                Description = "W zapomnianym przez świat szpitalu psychiatrycznym na amerykańskiej prowincji przetrzymywane są dzieci o zaburzeniach tak niebezpiecznych, że medycyna nie widzi dla nich ratunku. Przemoc fizyczna i psychiczna, gwałty i terror są tutejszą codziennością. Pewnego dnia grupa pacjentów ucieka, porywając młodą pielęgniarkę. Na ich czele stoi ten, który wkrótce krwawo zapracuje sobie na przydomek Leatherface. Tropem uciekinierów rusza ambitny i bezlitosny szeryf, który okaże się przeciwnikiem godnym najgorszego psychopaty."
            });

            movies.Add(new Movie
            {
                Category = drama,
                Title = "West of Her",
                DirectorId = 2,
                Duration = new TimeSpan(1, 30, 0),
                Description = "Samotny i zagubiony w życiu Dan (Ryan Caraway) zapisuje się do tajemniczej organizacji, która wysyła go w misję. Razem z enigmatyczną dziewczyną o imieniu Jane (Kelsey Siepser) ma on nocami zostawiać na ulicach amerykańskich miast tabliczki z zaszyfrowanym przesłaniem. Podczas podróży między mężczyzną a dziewczyną rodzi się intensywne uczucie. Dan jest jak otwarta książka, Jane jednak nie chce zbyt dużo mówić o sobie… Kameralne kino drogi w reżyserii dobrze rokującego debiutanta Ethana Warrena. Na zachód od niej to poetycka i pełna tajemnic opowieść, która mówi o potrzebie przynależności oraz poszukiwaniu sensu życia."
            });

            dbContext.SaveChanges();
        }
    }
}
