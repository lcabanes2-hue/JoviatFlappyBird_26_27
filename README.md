# Joviat Flappy Bird 26/27

Versió de Unity: 6000.3.25f1

## Controls

- Editor: Espai o clic per començar i per saltar.
- Android: tocar la pantalla per començar i per saltar.
- Botó Restart per tornar a jugar.

## Què he fet

- El fons es mou (paral·laxi), més lent que les canonades perquè sembli que està més lluny. Quan perds es para.
- Score i Best a la pantalla. El Best es guarda amb PlayerPrefs.
- Diàleg de nou rècord quan superes el Best.
- Sons d'inici, salt, punt, xoc i nou rècord.
- Animació de volar i de morir.
- L'ocell s'inclina cap amunt quan puja i cap avall quan cau.

## Paràmetres que he canviat

- Salt: ara poso la velocitat a 7 directament en lloc de fer AddForce, així cada salt és igual.
- Gravity Scale: de 1 a 2.5 perquè caigui més ràpid, com l'original.
- Velocitat màxima de caiguda: 10, perquè no caigui massa de pressa.
- Velocitat de les canonades: 2.5 multiplicat per Time.deltaTime, perquè vagi igual a qualsevol fps.
- Temps entre canonades: de 3 a 1.8 segons.
- Collider de l'ocell més petit i el de les canonades ajustat al dibuix, perquè els xocs coincideixin amb el que es veu.
- targetFrameRate a 60 perquè al mòbil vagi igual de fluid que a l'ordinador.
- Quan mors ja no pots saltar ni sumar punts.
