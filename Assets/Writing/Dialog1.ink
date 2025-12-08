/*<u><b>SCENE 1</u></b>
<i>(Encore une journée infernale...)</i>
...
<i>(J'ai hâte de rentrer... Est-ce que c'était ce soir la rediffusion?)</i>
...
<i>(J'espère que la pizzeria est encore ouverte...)</i>
...*/

____________________________

-> scene2

=== scene2 ===
<u><b>SCENE 2</u></b>

<b>INTERACTIONS</b>
-> Interactions

=== Interactions ===


+ TOURNIQUET
    -> Tourniquet
+ MACHINE A TICKETS
    -> Machine
+ RAILS
-> Rails
+ SIEGES
-> Siege
+ =>
-> end

=Tourniquet
*<u>Toucher</u>
<i>Le métal du tourniquet est froid au toucher.</i>
-> Tourniquet


*<u>Observer *</u>
<i>Où est-ce que j'ai mis ma carte...</i>
-> Tourniquet

*<u>Valider</u>
<i>Le "bip" habituel résonne dans le couloir vide.</i>
-> Tourniquet

+ =>
-> Interactions


=Machine
*<u>Observer *</u>
<i>J'ai déjà ma carte de Métro. La même depuis 15 ans.</i>
-> Machine

*<u>Utiliser</u>
<i>L'écran est cassé, et affiche une myriade de couleurs vives.<i/>
-> Machine

*<u>Toucher</u>
<i>Le clavier clique de façon satisfaisante.</i>
-> Machine

+ =>
-> Interactions


=Rails
*<u>Observer *</u>
<i>J'ai entendu dire que quelqu'un a sauté sur les rails ce matin... J'espère que la circulation a repris depuis.</i>
-> Rails

*<u>Toucher *</u>
<i>Je me demande si je suis le seul à avoir des pensées intrusives dans le genre...</i>
-> Rails

+ =>
-> Interactions


=Siege
*<u>Observer</u>
<i>Le rouge vif des sièges contraste avec le blanc des murs. Le plastique brille sous la lumière artificielle</i>
-> Siege

*<u>Toucher</u>
<i>Lisse et </i>
-> Siege

+ =>
-> Interactions







=== end ===
    -> END
