using System;
using System.Reflection;

namespace GoPay.Common
{
    public enum Country
    {
        [CountryCode(1)]CZE, // Czech Republic
        [CountryCode(2)]SVK, // Slovakia
        [CountryCode(100)]AFG, // Afghánistán
        [CountryCode(101)]ALA, // Ålandy
        [CountryCode(102)]ALB, // Albánie
        [CountryCode(103)]DZA, // Alžírsko
        [CountryCode(104)]ASM, // Americká Samoa
        [CountryCode(105)]VIR, // Americké Panenské ostrovy
        [CountryCode(106)]AND, // Andorra
        [CountryCode(107)]AGO, // Angola
        [CountryCode(108)]AIA, // Anguilla
        [CountryCode(109)]ATA, // Antarktida
        [CountryCode(110)]ATG, // Antigua a Barbuda
        [CountryCode(111)]ARG, // Argentina
        [CountryCode(112)]ARM, // Arménie
        [CountryCode(113)]ABW, // Aruba
        [CountryCode(114)]AUS, // Austrálie
        [CountryCode(115)]AZE, // Ázerbájdžán
        [CountryCode(116)]BHS, // Bahamy
        [CountryCode(117)]BHR, // Bahrajn
        [CountryCode(118)]BGD, // Bangladéš
        [CountryCode(119)]BRB, // Barbados
        [CountryCode(120)]BEL, // Belgie
        [CountryCode(121)]BLZ, // Belize
        [CountryCode(122)]BLR, // Bělorusko
        [CountryCode(123)]BEN, // Benin
        [CountryCode(124)]BMU, // Bermudy
        [CountryCode(125)]BTN, // Bhútán
        [CountryCode(126)]BOL, // Bolívie
        [CountryCode(127)]BES, // Bonaire, Svatý Eustach a Saba
        [CountryCode(128)]BIH, // Bosna a Hercegovina
        [CountryCode(129)]BWA, // Botswana
        [CountryCode(130)]BVT, // Bouvetův ostrov
        [CountryCode(131)]BRA, // Brazílie
        [CountryCode(132)]IOT, // Britské indickooceánské území
        [CountryCode(133)]VGB, // Britské Panenské ostrovy
        [CountryCode(134)]BRN, // Brunej
        [CountryCode(135)]BGR, // Bulharsko
        [CountryCode(136)]BFA, // Burkina Faso
        [CountryCode(137)]BDI, // Burundi
        [CountryCode(138)]COK, // Cookovy ostrovy
        [CountryCode(139)]CUW, // Curaçao
        [CountryCode(140)]TCD, // Čad
        [CountryCode(141)]MNE, // Černá Hora
        [CountryCode(143)]CHN, // Čína
        [CountryCode(144)]DNK, // Dánsko
        [CountryCode(145)]COD, // Demokratická republika Kongo
        [CountryCode(146)]DMA, // Dominika
        [CountryCode(147)]DOM, // Dominikánská republika
        [CountryCode(148)]DJI, // Džibutsko
        [CountryCode(149)]EGY, // Egypt
        [CountryCode(150)]ECU, // Ekvádor
        [CountryCode(151)]ERI, // Eritrea
        [CountryCode(152)]EST, // Estonsko
        [CountryCode(153)]ETH, // Etiopie
        [CountryCode(154)]FRO, // Faerské ostrovy
        [CountryCode(155)]FLK, // Falklandy (Malvíny)
        [CountryCode(156)]FJI, // Fidži
        [CountryCode(157)]PHL, // Filipíny
        [CountryCode(158)]FIN, // Finsko
        [CountryCode(159)]FRA, // Francie
        [CountryCode(160)]GUF, // Francouzská Guyana
        [CountryCode(161)]ATF, // Francouzská jižní a antarktická území
        [CountryCode(162)]PYF, // Francouzská Polynésie
        [CountryCode(163)]GAB, // Gabon
        [CountryCode(164)]GMB, // Gambie
        [CountryCode(165)]GHA, // Ghana
        [CountryCode(166)]GIB, // Gibraltar
        [CountryCode(167)]GRD, // Grenada
        [CountryCode(168)]GRL, // Grónsko
        [CountryCode(169)]GEO, // Gruzie
        [CountryCode(170)]GLP, // Guadeloupe
        [CountryCode(171)]GUM, // Guam
        [CountryCode(172)]GTM, // Guatemala
        [CountryCode(173)]GIN, // Guinea
        [CountryCode(174)]GNB, // Guinea-Bissau
        [CountryCode(175)]GGY, // Guernsey
        [CountryCode(176)]GUY, // Guyana
        [CountryCode(177)]HTI, // Haiti
        [CountryCode(178)]HMD, // Heardův ostrov a McDonaldovy ostrovy
        [CountryCode(179)]HND, // Honduras
        [CountryCode(180)]HKG, // Hongkong
        [CountryCode(181)]CHL, // Chile
        [CountryCode(182)]HRV, // Chorvatsko
        [CountryCode(183)]IND, // Indie
        [CountryCode(184)]IDN, // Indonésie
        [CountryCode(185)]IRQ, // Irák
        [CountryCode(186)]IRN, // Írán
        [CountryCode(187)]IRL, // Irsko
        [CountryCode(188)]ISL, // Island
        [CountryCode(189)]ITA, // Itálie
        [CountryCode(190)]ISR, // Izrael
        [CountryCode(191)]JAM, // Jamajka
        [CountryCode(192)]JPN, // Japonsko
        [CountryCode(193)]YEM, // Jemen
        [CountryCode(194)]JEY, // Jersey
        [CountryCode(195)]ZAF, // Jihoafrická republika
        [CountryCode(196)]SGS, // Jižní Georgie a Jižní Sandwichovy ostrovy
        [CountryCode(197)]KOR, // Jižní Korea
        [CountryCode(198)]SSD, // Jižní Súdán
        [CountryCode(199)]JOR, // Jordánsko
        [CountryCode(200)]CYM, // Kajmanské ostrovy
        [CountryCode(201)]KHM, // Kambodža
        [CountryCode(202)]CMR, // Kamerun
        [CountryCode(203)]CAN, // Kanada
        [CountryCode(204)]CPV, // Kapverdy
        [CountryCode(205)]QAT, // Katar
        [CountryCode(206)]KAZ, // Kazachstán
        [CountryCode(207)]KEN, // Keňa
        [CountryCode(208)]KIR, // Kiribati
        [CountryCode(209)]CCK, // Kokosové ostrovy
        [CountryCode(210)]COL, // Kolumbie
        [CountryCode(211)]COM, // Komory
        [CountryCode(212)]COG, // Kongo
        [CountryCode(213)]CRI, // Kostarika
        [CountryCode(214)]CUB, // Kuba
        [CountryCode(215)]KWT, // Kuvajt
        [CountryCode(216)]CYP, // Kypr
        [CountryCode(217)]KGZ, // Kyrgyzstán
        [CountryCode(218)]LAO, // Laos
        [CountryCode(219)]LSO, // Lesotho
        [CountryCode(220)]LBN, // Libanon
        [CountryCode(221)]LBR, // Libérie
        [CountryCode(222)]LBY, // Libye
        [CountryCode(223)]LIE, // Lichtenštejnsko
        [CountryCode(224)]LTU, // Litva
        [CountryCode(225)]LVA, // Lotyšsko
        [CountryCode(226)]LUX, // Lucembursko
        [CountryCode(227)]MAC, // Macao
        [CountryCode(228)]MDG, // Madagaskar
        [CountryCode(229)]HUN, // Maďarsko
        [CountryCode(230)]MKD, // Makedonie
        [CountryCode(231)]MYS, // Malajsie
        [CountryCode(232)]MWI, // Malawi
        [CountryCode(233)]MDV, // Maledivy
        [CountryCode(234)]MLI, // Mali
        [CountryCode(235)]MLT, // Malta
        [CountryCode(236)]IMN, // Ostrov Man
        [CountryCode(237)]MAR, // Maroko
        [CountryCode(238)]MHL, // Marshallovy ostrovy
        [CountryCode(239)]MTQ, // Martinik
        [CountryCode(240)]MUS, // Mauricius
        [CountryCode(241)]MRT, // Mauritánie
        [CountryCode(242)]MYT, // Mayotte
        [CountryCode(243)]UMI, // Menší odlehlé ostrovy USA
        [CountryCode(244)]MEX, // Mexiko
        [CountryCode(245)]FSM, // Mikronésie
        [CountryCode(246)]MDA, // Moldavsko
        [CountryCode(247)]MCO, // Monako
        [CountryCode(248)]MNG, // Mongolsko
        [CountryCode(249)]MSR, // Montserrat
        [CountryCode(250)]MOZ, // Mosambik
        [CountryCode(251)]MMR, // Myanmar
        [CountryCode(252)]NAM, // Namibie
        [CountryCode(253)]NRU, // Nauru
        [CountryCode(254)]DEU, // Německo
        [CountryCode(255)]NPL, // Nepál
        [CountryCode(256)]NER, // Niger
        [CountryCode(257)]NGA, // Nigérie
        [CountryCode(258)]NIC, // Nikaragua
        [CountryCode(259)]NIU, // Niue
        [CountryCode(260)]NLD, // Nizozemsko
        [CountryCode(261)]NFK, // Norfolk
        [CountryCode(262)]NOR, // Norsko
        [CountryCode(263)]NCL, // Nová Kaledonie
        [CountryCode(264)]NZL, // Nový Zéland
        [CountryCode(265)]OMN, // Omán
        [CountryCode(266)]PAK, // Pákistán
        [CountryCode(267)]PLW, // Palau
        [CountryCode(268)]PSE, // Palestinská autonomie
        [CountryCode(269)]PAN, // Panama
        [CountryCode(270)]PNG, // Papua-Nová Guinea
        [CountryCode(271)]PRY, // Paraguay
        [CountryCode(272)]PER, // Peru
        [CountryCode(273)]PCN, // Pitcairnovy ostrovy
        [CountryCode(274)]CIV, // Pobřeží slonoviny
        [CountryCode(275)]POL, // Polsko
        [CountryCode(276)]PRI, // Portoriko
        [CountryCode(277)]PRT, // Portugalsko
        [CountryCode(278)]AUT, // Rakousko
        [CountryCode(279)]REU, // Réunion
        [CountryCode(280)]GNQ, // Rovníková Guinea
        [CountryCode(281)]ROU, // Rumunsko
        [CountryCode(282)]RUS, // Rusko
        [CountryCode(283)]RWA, // Rwanda
        [CountryCode(284)]GRC, // Řecko
        [CountryCode(285)]SPM, // Saint-Pierre a Miquelon
        [CountryCode(286)]SLV, // Salvador
        [CountryCode(287)]WSM, // Samoa
        [CountryCode(288)]SMR, // San Marino
        [CountryCode(289)]SAU, // Saúdská Arábie
        [CountryCode(290)]SEN, // Senegal
        [CountryCode(291)]PRK, // Severní Korea
        [CountryCode(292)]MNP, // Severní Mariany
        [CountryCode(293)]SYC, // Seychely
        [CountryCode(294)]SLE, // Sierra Leone
        [CountryCode(295)]SGP, // Singapur
        [CountryCode(297)]SVN, // Slovinsko
        [CountryCode(298)]SOM, // Somálsko
        [CountryCode(299)]ARE, // Spojené arabské emiráty
        [CountryCode(300)]GBR, // Spojené království
        [CountryCode(301)]USA, // Spojené státy americké
        [CountryCode(302)]SRB, // Srbsko
        [CountryCode(303)]CAF, // Středoafrická republika
        [CountryCode(304)]SDN, // Súdán
        [CountryCode(305)]SUR, // Surinam
        [CountryCode(306)]SHN, // Svatá Helena, Ascension a Tristan da Cunha
        [CountryCode(307)]LCA, // Svatá Lucie
        [CountryCode(308)]BLM, // Svatý Bartoloměj
        [CountryCode(309)]KNA, // Svatý Kryštof a Nevis
        [CountryCode(310)]MAF, // Svatý Martin (francouzská část)
        [CountryCode(311)]SXM, // Svatý Martin (nizozemská část)
        [CountryCode(312)]STP, // Svatý Tomáš a Princův ostrov
        [CountryCode(313)]VCT, // Svatý Vincenc a Grenadiny
        [CountryCode(314)]SWZ, // Svazijsko
        [CountryCode(315)]SYR, // Sýrie
        [CountryCode(316)]SLB, // Šalamounovy ostrovy
        [CountryCode(317)]ESP, // Španělsko
        [CountryCode(318)]SJM, // Špicberky a Jan Mayen
        [CountryCode(319)]LKA, // Šrí Lanka
        [CountryCode(320)]SWE, // Švédsko
        [CountryCode(321)]CHE, // Švýcarsko
        [CountryCode(322)]TJK, // Tádžikistán
        [CountryCode(323)]TZA, // Tanzanie
        [CountryCode(324)]THA, // Thajsko
        [CountryCode(325)]TWN, // Tchaj-wan
        [CountryCode(326)]TGO, // Togo
        [CountryCode(327)]TKL, // Tokelau
        [CountryCode(328)]TON, // Tonga
        [CountryCode(329)]TTO, // Trinidad a Tobago
        [CountryCode(330)]TUN, // Tunisko
        [CountryCode(331)]TUR, // Turecko
        [CountryCode(332)]TKM, // Turkmenistán
        [CountryCode(333)]TCA, // Turks a Caicos
        [CountryCode(334)]TUV, // Tuvalu
        [CountryCode(335)]UGA, // Uganda
        [CountryCode(336)]UKR, // Ukrajina
        [CountryCode(337)]URY, // Uruguay
        [CountryCode(338)]UZB, // Uzbekistán
        [CountryCode(339)]CXR, // Vánoční ostrov
        [CountryCode(340)]VUT, // Vanuatu
        [CountryCode(341)]VAT, // Vatikán
        [CountryCode(342)]VEN, // Venezuela
        [CountryCode(343)]VNM, // Vietnam
        [CountryCode(344)]TLS, // Východní Timor
        [CountryCode(345)]WLF, // Wallis a Futuna
        [CountryCode(346)]ZMB, // Zambie
        [CountryCode(347)]ESH, // Západní Sahara
        [CountryCode(348)]ZWE, // Zimbabwe
        [CountryCode(345)]UNK  // UNMIK - ISO numeric code 900 - United Nations Interim Administration Mission in Kosovo


    }

    public static class CountryExtension
    {

        public static int GetCountryCode(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (CountryCode)fieldInfo.GetCustomAttribute(typeof(CountryCode));
            return attribute.Code;
        }
    }

    public class CountryCode : Attribute
    {
        public int Code { get; set; }

        public CountryCode(int code) : base()
        {
            Code = code;
        }
    }

}
