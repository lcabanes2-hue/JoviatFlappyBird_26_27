# Joviat Flappy Bird 26/27

Versió ampliada del Flappy Bird que hem treballat a classe.

- **Versió de Unity:** 6000.3.25f1 (Unity 6.3), plantilla 2D URP
- **Plataformes:** editor de Unity i Android (APK)
- **Input:** Active Input Handling en mode *Both* (input antic)

## Controls

| Acció | Editor | Android |
|---|---|---|
| Començar la partida | Botó **START** o tecla **Espai** | Botó **START** o un toc a la pantalla |
| Impuls de l'ocell | Tecla **Espai** | Toc a la pantalla |
| Tornar a jugar | Botó **Restart** | Botó **Restart** |

## Funcionalitats

1. **Fons amb paral·laxi** (`Background.cs`). Hi ha dues còpies del fons, una al costat de l'altra. Es mouen a 0,5 u/s, cinc vegades més a poc a poc que les canonades (2,5 u/s), i això dona sensació de profunditat. Quan una còpia surt per l'esquerra, es mou dues amplades a la dreta, de manera que el desplaçament és continu i no queden forats. Les còpies se solapen 0,02 unitats perquè no es vegi la línia d'unió. Quan s'acaba la partida, el fons s'atura.
2. **Puntuació i rècord** (`GameManager.cs`). Sempre es veuen *Score* i *Best*. Cada parella de canonades té un trigger que només suma un punt, gràcies a la variable `scored`. El rècord es desa amb `PlayerPrefs` (clau `BestScore`) i `PlayerPrefs.Save()`, i es llegeix amb `PlayerPrefs.GetInt("BestScore", 0)`, de manera que si encara no n'hi ha cap val 0. En reiniciar, l'escena es torna a carregar i la puntuació torna a 0.
3. **Diàleg de nou rècord.** Surt en acabar la partida, només si la puntuació **supera** el rècord (`score > bestScore`; si l'iguala, no). Mostra la puntuació aconseguida i fa sonar el so de rècord. Es crida amb `Invoke` 0,6 segons després del xoc, perquè els dos sons no se solapin. Com que `StopGame()` només s'executa una vegada per partida (variable `gameOver`), el diàleg tampoc pot sortir dues vegades.
4. **Efectes de so** (fitxers a `Assets/Audio`). El GameManager té un `AudioSource` i els reprodueix amb `PlayOneShot`, cadascun amb el seu volum:

   | So | Quan sona | Volum |
   |---|---|---|
   | Impuls | a cada impuls durant la partida | 0,5 |
   | Col·lisió | en xocar amb una canonada o un límit | 0,9 |
   | Inici | en començar, amb el botó START o un toc | 0,7 |
   | Nou rècord | quan apareix el diàleg | 0,8 |
   | Punt | quan se supera una parella de canonades | 0,6 |

   El toc que comença la partida només fa sonar el so d'inici, no el d'impuls.
5. **Animacions.** L'Animator `Player.controller` té els estats *Flying*, amb les ales en bucle, i *Die*. Quan l'ocell xoca, `Player.Die()` fa `animator.SetTrigger("Die")`. En reiniciar es torna a carregar l'escena i l'ocell torna a *Flying*.
6. **Inclinació** (`Player.cs`). L'angle es calcula a partir de la velocitat vertical: `Mathf.Clamp(velocitat * 8, -75, 30)`. L'ocell mira amunt quan puja (fins a 30°) i avall quan cau (fins a -75°), i mai fa una volta sencera. La transició és suau amb `Quaternion.Lerp` multiplicat per `Time.deltaTime`.

## Ajust de la física i la jugabilitat

| Paràmetre | Abans | Ara | Per què |
|---|---|---|---|
| Impuls (`Player.force`) | `AddForce` de 3 | **velocitat = 7** | Ara l'impuls és immediat i sempre igual. Abans se sumava a la velocitat de caiguda, i si l'ocell queia ràpid gairebé no pujava. |
| Gravetat (`Gravity Scale`) | 1 | **2,5** | La caiguda és més ràpida i progressiva, com a l'original. Amb impuls 7, cada toc fa pujar l'ocell aproximadament 1 unitat. |
| Velocitat màxima de caiguda | — | **10** | Evita que l'ocell acceleri sense límit. |
| Velocitat de les canonades (`Pipe.speed`) | 0,02 per frame | **2,5 × `Time.deltaTime`** | Ara la velocitat no depèn dels fps. Abans anaven massa lentes. |
| Temps entre canonades (`timeToSpawn`) | 3 s | **1,8 s** | Amb 2,5 u/s queden separades unes 4,5 unitats: hi ha temps per reaccionar, però sense pauses llargues. |
| Pas entre canonades | ~1,7 u | ~1,8 u (≈3 cops l'ocell) | És estret com a l'original, però sempre es pot passar. El rang d'alçades del spawner s'ha ajustat perquè el pas sempre quedi dins la pantalla. |
| On apareixen les canonades | x = 4,8 (dins la pantalla) | **x = 12** | Les canonades ja no apareixen de cop a la vista, ni tan sols en mòbils allargats. |
| Collider de l'ocell | caixa de la mida del sprite | **càpsula al 85 %** | Les cantonades transparents del dibuix ja no provoquen xocs que no es veuen. |
| Colliders de les canonades | més amples que el sprite | **mida exacta del sprite** | Els xocs coincideixen amb el que es veu. |
| `Application.targetFrameRate` | 30 a Android | **60** | El joc va igual de fluid a l'editor i al mòbil. |
| Orientació d'Android | Automàtica | **Només horitzontal** | El joc està pensat en format apaïsat. |

Un cop l'ocell ha mort, `gameOver` és `true`: l'ocell ja no rep impulsos, `AddScore` no suma punts, i les canonades i el fons s'aturen.

## Scripts

- `GameManager.cs`: estat de la partida, puntuació, rècord, UI i sons.
- `Player.cs`: entrada (espai o toc), impuls, inclinació i animació de mort.
- `Pipe.cs`: moviment de les canonades i punt en passar-les.
- `PipeSpawner.cs`: crea canonades amb `InvokeRepeating`.
- `PipeCollider.cs` i `Border.cs`: xocs que acaben la partida.
- `Background.cs`: fons amb paral·laxi.

## Generar l'APK

1. A **File > Build Profiles**, tria **Android** i fes **Switch Platform**.
2. Comprova que la `SampleScene` és a la llista d'escenes.
3. Fes **Build** i desa el fitxer `.apk`.
