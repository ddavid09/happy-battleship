# happy-battleship
Recruitment task for Happy Team

Komentarz na dzień 23.04.2021.
Zadanie nie jest jeszcze ukończone, pomimo tego że w commcie 'Fix ships contact corrners and sides' można zobaczyć praktycznie dziłającą wersję zadania - następuje wymiana losowych "strzałów" pomiędzy obiema stronami symulacji, to po dojściu do tego etapu chciałem uczynić wybieranie nowego miejsca strzału bardziej "intelignetnym" (np. aby po trafionym strzale ostrzeliwać sąsiednie pola w takim celu aby najpierw określić kierunek statku a potem a doprowadzić do jego zatopienia). Niestety próby pracy nad tą funkcjonalnością doprowadzały do powstawania zupełnego spaghetti, więc porzuciłem to zadanie na rzecz zrefaktorowania kodu i opatrzenia go większą ilością abstrakcji - aby przy ponownych próbach nie gubić się w kodzie. Po zrobieniu tego rozpocząłem walkę z prawidłową komunikacją frontend-backend z wykorzystaniem już stworzonej abstrakcji i wbudowanego w asp.net core kontenera IoC ponieważ porzednie rozwiązanie (działające z w/w commita) działało na czymś co na 101% jest anty-patternem - nie wiem jak to można nazwać - chyba coś a'la 'await while' - polecam zerknąć jak chcecie się pośmiać 😁 - (metoda StartSimulation() w BattleshipHub w w/w commicie). 
Walka była nie równa, i niestety nie zdąrzyłem do chwili obecnej znaleźć dobrej metody komunikacji między frontendem i backendem - oczywiście jest to kwestia czasu. 

Nie zdąrzyłem ukończyć całego zadania do terminu jaki zadeklarowałem, nie mniej planuję to zadanie ukończyć w przyszłym tygodniu. 

Kroki jakie podejmę w pierwszej kolejności w przyszłym tygodniu to komunikacja frontend-backend a później utworzenie inteligentnej strategii wylosowania nowego strzału. 

