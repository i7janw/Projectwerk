/*
	Test database voor project laagspanningsnet
*/
DROP SCHEMA IF EXISTS `Laagspanningsnet` ;

CREATE DATABASE `Laagspanningsnet` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;
USE `Laagspanningsnet`;

/*
Principe : 

In de plaats van het laagspanningsnet te bekijken uit een combinatie H-sleutels, VB's en Zekeringkasten,
bekijken we het laagspanningsnet uit een combinatie van Aansluitpunten die verbonden zijn met elkaar door 
middel van Aansluitingen.

De database bestaat dan uit 3 tables:
 - Aansluitpunten : Transfo's, Verdeelborden, Zekeringkasten
 - Aansluitingen  : Verbindingen tussen een vertrek van een aansluitpunt en een ander aansluitpunt
 - Machines       : Machines

*/
CREATE TABLE `Aansluitpunten` (
  /* Aansluitpunt, T8, VB810, K810a */
  `AP_id` varchar(10) NOT NULL,				
  /* Referentie naar plaats (grondplannummer) */
  `AP_locatie` varchar(10) NULL,			
  PRIMARY KEY (`AP_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Machines` (
  /* Aansluitpunt, T8, VB810, K810a */
  `M_id` varchar(10) NOT NULL,				
  /* Omschrijving van het aansluitpunt */
  `M_omschrijving` varchar(80) NOT NULL,	
  /* Referentie naar plaats (grondplannummer) */
  `M_locatie` varchar(10) NULL,				
  PRIMARY KEY (`M_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Aansluitingen` (
  /* Over welke aansluiting gaat het?  bv. Sa, H810, Kring 3.1 */
  `A_id` varchar(10) NOT NULL,			
  /* Van welk AansluitPunt is deze Aansluiting afkomstig */
  `AP_id` varchar(10) NOT NULL,			
  /* Naar welk aansluitpunt gaat deze aansluiting?
	 Kan NULL zijn als deze aansluiting niet naar een ander aansluitpunt gaat */
  `Naar_AP_id` varchar(10) NULL, 			
  /* Naar welke machine gaat deze aansluiting?
     Kan NULL zijn als deze aansluiting niet naar een machine gaat */
  `Naar_M_id` varchar(10) NULL,				
  /* Omschrijving van hetgene er aangesloten is, 
     gebruiken als AP_id en M_id == NULL, 
     als Mid <>0 dan M_omschrijving gebruiken */
  `Omschrijving` varchar(80) NULL,
  /* KabelType, bv. XVB */
  `Kabeltype` varchar(7) NULL,	
  /* KabelDoorMeter bv. 4G95 */
  `Kabelsectie` varchar(12) NULL,	
  /* Zekering in A */
  `Stroom` SMALLINT NULL,					
  /* Uit hoeveel polen bestaat deze aansluiting */
  `Polen` TINYINT NOT NULL,					
    
  PRIMARY KEY (`A_id`,`AP_id`),
  CONSTRAINT `AP_id` 			FOREIGN KEY (`AP_id`) 			
  REFERENCES `Aansluitpunten` (`AP_id`)		ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Naar_AP_id`  		FOREIGN KEY (`Naar_AP_id`) 		
  REFERENCES `Aansluitpunten` (`AP_id`) 	ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Naar_M_id`   		FOREIGN KEY (`Naar_M_id`) 		
  REFERENCES `Machines` (`M_id`)		    ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*
Invoegen van testwaarden, om theorie te testen ;-) 
*/

INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('T8', 'X80');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('T2', 'X20');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('T4', 'X40');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('T1', 'X10');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('T7', 'X70');

INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('VB810', 'F15');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('K810a', 'G28');
INSERT INTO `Laagspanningsnet`.`Aansluitpunten` 
(`AP_id`, `AP_lOcatie`) VALUES ('K810b', 'G27');

INSERT INTO `Laagspanningsnet`.`Machines` 
(`M_id`, `M_omschrijving`, `M_locatie`) VALUES ('S019', 'Fromag Steekmachine', 'G26');

INSERT INTO `Laagspanningsnet`.`Machines` 
(`M_id`, `M_omschrijving`, `M_locatie`) VALUES ('M169', 'Etscabine', 'F31');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('T8', 'H801', NULL, 'M169', NULL, 'XVB', '4G70', '160', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('T8', 'H802', NULL, NULL, 'Flakt (is dit wel juist? Is toch verwijderd?)', 'XVB', '3x70+35', '160', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('T8', 'H810', 'VB810', NULL, NULL, 'XVB', '4G95', '250', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('VB810', 'Sa', 'K810a', NULL, NULL, 'XVB', '4G50', '125', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('VB810', 'Sb', 'K810b', NULL, NULL, 'XVB', '4G50', '125', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('K810a', '1.1', NULL, NULL , 'Draaiarm', 'XVB', '5G2.5', '20', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('K810a', '1.2', NULL, NULL, 'Stopkontaktenblok', 'XVB', '5G2.5', '20', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('K810a', '3.1', NULL, 'S019', NULL, 'XVB', '5G6', '40', '3');

INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
`Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
VALUES ('K810a', '3.4', NULL, NULL, 'Stopcontactenblok (geel model)', 'XVB', '5G16', '63', '3');
/*EOF*/