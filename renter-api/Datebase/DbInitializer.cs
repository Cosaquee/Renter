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

            dbContext.SaveChanges();
        }

        private async Task SeedBooks()
        {
            if (books == null || await books.AnyAsync())
            {
                return;
            }

            var pawelKornew = await authors.FirstOrDefaultAsync(x => x.Name == "Paweł");
            var fantasy = await categories.FirstOrDefaultAsync(x => x.Name == "Fantasy");
            books.Add(new Book { Title = "Lodowa Cytadela", ISBN = "9788379640935", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "Przygranicze to parę miast kilkadziesiąt lat temu wyrwanych z naszego świata i przeniesionych do krainy wiecznego mrozu. Dziwne miejsce, w którym przez większą część roku panują chłody i wieją lodowate wiatry, a ludzie nabywają magicznych zdolności, co czyni ich bardziej niebezpiecznymi od uzbrojonych po zęby bojowników. Lecz człowiek umie przystosować się do każdych warunków, więc Jewgienij Apostoł już dawno przestał żałować, że trafił do Fortu - dawnego prowincjonalnego miasteczka, które stało się centrum cywilizacji na tej pokrytej śniegiem ziemi. I nawet kiedy przyszło wziąć nogi za pas, aby ratować skórę przed najemnymi zabójcami, przede wszystkim pomyślał o tym, jak nie zmarnować szansy na życiowy interes.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/lodowa-cytadela.jpg" });
            books.Add(new Book { Title = "Czarne Południe", ISBN = "9788375747164", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "Przy Soplu twardziele topnieją \n Jeżeli nie zabije cię kula, to dobije cię czar. Nie osmali cię ogień, to skapiejesz na mrozie. Oderwanym skrawkiem naszego świata wstrząsa brutalna wojna. Stary i kruchy porządek ostatecznie się zawalił - czas wybrać nowych władców. Ręka, która utrzyma nóż, rządzić będzie Przygraniczem. \n I dlatego wszyscy, którzy chcą się liczyć w nowym układzie sił szukają Sopla. \n Sopel, by przeżyć i wyrwać się z Przygranicza musi szukać noża. \n Napalm, jak na piromantę przystało, szuka okazji do rozróby. \n Zanim nastanie Czarne Południe wszystkim uczestnikom te poszukiwania bokiem wylezą. Niektórym razem z flakami.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/czarne-poludnie.jpg" });
            books.Add(new Book { Title = "Sopel", ISBN = "9788379642267", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "W świat Przygranicza trafiasz przypadkiem. Z woli losu. Wysiadasz z wagonu, by na postoju napić się piwa. Chwilę potem nie ma ani stacji, ani pociągu, ani torów, ani dworcowej knajpy, z której właśnie wyszedłeś. \n Tylko mróz, śnieg, zamieć. I konieczność toczenia ciągłych walk. Z ludźmi i innymi bestiami. \n Światem Przygranicza rządzi zasada „wszyscy przeciwko wszystkim”.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/sopel.jpg" });
            books.Add(new Book { Title = "Sopel", ISBN = "9788379642267", AuthorId = pawelKornew.Id, CategoryId = fantasy.Id, Description = "W świat Przygranicza trafiasz przypadkiem. Z woli losu. Wysiadasz z wagonu, by na postoju napić się piwa. Chwilę potem nie ma ani stacji, ani pociągu, ani torów, ani dworcowej knajpy, z której właśnie wyszedłeś. \n Tylko mróz, śnieg, zamieć. I konieczność toczenia ciągłych walk. Z ludźmi i innymi bestiami. \n Światem Przygranicza rządzi zasada „wszyscy przeciwko wszystkim”.", CoverURL = "https://s3.eu-central-1.amazonaws.com/cmsrental/books/sopel.jpg" });

            var bartekBiedrzycki = await authors.FirstOrDefaultAsync(x => x.Name == "Bartek");
            var scifi = await categories.FirstOrDefaultAsync(x => x.Name == "Sci-fi");
            books.Add(new Book { Title = "Dworzec Śródmieście", ISBN = "9788379641825", AuthorId = bartekBiedrzycki.Id, CategoryId = scifi.Id, Description = "Warszawa po Zagładzie to o wiele więcej, niż metro. Ludzie żyją tu na podziemnych dworcach, w piwnicach, w zapomnianych przejściach. Mieszkańcy tych miejsc śmieją się i płaczą, kochają i nienawidzą, czekają – czasem sami nie wiedzą na co; szukają – rzadko wiedząc czego. Żyją i umierają. Ale czasem, kiedy umierają zbyt gwałtownie, krucha równowaga i prowizoryczna umowa społeczna zostają naruszone. Czasem za krew można zapłacić nabojami, czasem książkami, a czasem odpłatą może być tylko krew.  Czy po wojnie atomowej nie ma już dobrych ludzi? Czy każdy człowiek, nawet ten z pozoru najlepszy, ma w sobie utajonego zbrodniarza, który za godziwą zapłatę zrobi wszystko? Ile warte jest w takim świecie życie – życie brata, życie męża, życie syna... Napisana z wielkim rozmachem kulminacja przygód rudobrodego stalkera Borki z Kryształowego Pałacu, pokazująca życie Warszawy poza systemem stołecznego metra od chwili Zagłady, aż po czasy najnowsze. Klucz do zrozumienia wydarzeń trylogii warszawskiej, odkrywający wszystkie tajemnice i misterną siatkę powiązań między pozornie nieznajomymi bohaterami." });

            var danBrown = await authors.FirstOrDefaultAsync(x => x.Name == "Dan");
            var thriller = await categories.FirstOrDefaultAsync(x => x.Name == "Thriller");
            books.Add(new Book { Title = "Początek", ISBN = "9788381101431", AuthorId = danBrown.Id, CategoryId = thriller.Id, Description = "Robert Langdon, profesor Uniwesytetu Harvarda i specjalista od symboli, przenosi nad do malowniczej Hiszpanii. Tam przemierza ulice Madrytu, Barcelony, Sewilli i Builbao w poszukiwaniu odpowiedzi na nurtujące ludzkość pytania: \n Skąd pochodzimy? \n Dokąd zmierzamy? \n Brown zapełnił karty tej fascynującej powiesci kodami, symbolami, odkryciami naukowymi, ciekawostkami religijnymi, historycznymi i architektonicznymi. Powiewu świezości dodają opisy dzieł sztuki współczesnej i najnowocześniejszych rozwiązań technologicznych! \n Bez względu a to, kim jesteś i w co wierzysz, nie masz wpływu na to, co się wydarzy..." });

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
