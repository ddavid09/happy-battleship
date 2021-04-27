# happy-battleship
Recruitment task for Happy Team

### Komentarz na dzień 27.04.2021.
W celu rozwiązania poprzednich problemów związanych z komunikacją frontend-backend w pierwszej kolejności aby mieć pewność że cała logika działa poprawnie - a przynajmniej że wykonuje się od początku do końca utworzyłem projekt w konsoli w którym mogłem uruchomić symulację i zobaczyć logi z jej przebiegu. Całą logikę przeniosłem do Class Library, po tym jak udało się uruchomić poprawny przebieg symulacji w konsoli, postanowiłem podejść jeszcze raz do signalR'a tym razem się udało. Klasa odpowiedzialna na przebieg symulacji dziedziczy po klasie symulacji z biblioteki i rozszerza ją o id klienta (aplikacji klienckiej) z którym ta symulacja jest powiązana dzięki wstrzyknięciu zależności IHubContext może przy kolejnych zdarzeniach występujących w symulacji wywoływać metody po stronie klienta (o zapisanym ID). W dalszej kolejności będę chciał popracować trochę nad wyglądem i kodem w aplikacji klienckiej bo szału nie ma... potem chciałbym stworzyć bardziej inteligentną strategię strzelania przez 'playerów'.

Mały disclaimer, w strukturze na githubie istnieją dwa foldery HappyBattleship.web i .Web (powinien być tylko .Web) - to chyba jakiś bug po stronie githuba bo po sklonowaniu repo wszystko jest Sikalafą 😁

### Komentarz na dzień 23.04.2021 (dzień wysłania e-mail z linkiem do repo).

Zadanie nie jest jeszcze ukończone, pomimo tego że w commcie '(f0c52ba6b69e1bf970444e14675b12805075ec55)Fix ships contact corrners and sides' można zobaczyć praktycznie działającą wersję zadania - następuje wymiana losowych "strzałów" pomiędzy obiema stronami symulacji, to po dojściu do tego etapu chciałem uczynić wybieranie nowego miejsca strzału bardziej "inteligentnym" (np. aby po trafionym strzale ostrzeliwać sąsiednie pola w takim celu aby najpierw określić kierunek statku a potem a doprowadzić do jego zatopienia). Niestety próby pracy nad tą funkcjonalnością doprowadzały do powstawania zupełnego spaghetti, więc porzuciłem to zadanie na rzecz zrefaktoryzowania kodu i opatrzenia go większą ilością abstrakcji - aby przy ponownych próbach nie gubić się w kodzie. Po zrobieniu tego rozpocząłem walkę z prawidłową komunikacją frontend-backend z wykorzystaniem już stworzonej abstrakcji i wbudowanego w asp.net core kontenera IoC ponieważ poprzednie rozwiązanie (działające z w/w commita) działało na czymś co na 101% jest anty-patternem - nie wiem jak to można nazwać - chyba coś a'la 'await while' - polecam zerknąć jak chcecie się pośmiać 😁 - (metoda StartSimulation() w BattleshipHub w w/w commicie). Walka była nie równa, i niestety nie zdążyłem do chwili obecnej znaleźć dobrej metody komunikacji między frontendem i backendem - oczywiście jest to kwestia czasu.
Nie zdążyłem ukończyć całego zadania do terminu jaki zadeklarowałem, nie mniej planuję to zadanie ukończyć w przyszłym tygodniu.

Kroki jakie podejmę w pierwszej kolejności w przyszłym tygodniu to komunikacja frontend-backend a później utworzenie inteligentnej strategii wylosowania nowego strzału.

Jeżeli nie zraża was fakt że nie wyrobiłem się w 100% to z miłą chęcią prześlę rozwiązanie, gdy uda mi się je całkowicie ukończyć - jeśli można to na tę chwilę proszę o feedback 😃.

