# DeleteVKFavorites

## Сборка
### Linux
Для начала ставим моно по инструкции с офф сайта: https://www.mono-project.com/download/stable/#download-lin

После клонируем код к себе 

```git clone https://github.com/cortelf/DeleteVKFavorites.git .```

Переходим в папку с основным проектом

```cd DeleteVKFavorites/DeleteVKFavorites```

Собираем

``` msbuild DeleteVKFavorites.sln```

### Windows
Ставим (если нет) https://www.microsoft.com/ru-RU/download/details.aspx?id=56116

И ставим VS 2017
## Запуск
### Linux
Идем в папку с бинарником

```cd DeleteVKFavorites/bin/Debug```

Даем права для запуска

```chmod +x DeleteVKFavorites.exe```

Запускаем

```./DeleteVKFavorites.exe```

### Windows
Запускаем в VS или идем в 

```DeleteVKFavorites/bin/Debug```

И запускаем

```DeleteVKFavorites.exe```
