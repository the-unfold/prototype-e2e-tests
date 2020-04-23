# e2e tests

Идея в следующем: при локальной разработке куча микросервисов поднимается на в kube, а локально в докере, при этом все микеросервисы лежат рядом в одной репе и поднимаются вместе одним docker-compose.

Для того, чтобы не плодить демо-данные и заглушки, которые имитируют действия пользователей (эти действия влияют более чем на один сервис), можно сделать исполняемые сценарии прользовательских действий, которые будут в реальном приложении в реальном браузере проходить регистрацию, подтверждать контакты, заполнять свои профили и общаться друг с другом. И периодически выполнять какие-то ещё новые действия. Для написания таких сценариев очень круто подходит [Canopy](https://lefthandedgoat.github.io/canopy/) - Selenium под капотом, крутой DSL для взаимодействия "пользователя" с браузером. Сценарии могут композироваться и параметризоваться. А поскольку это в первую очередь инструмент для e2e-тестов, то можно переиспользовать сценарии пользовательских действий как для локальной разработки (накатывая их вручную), так и для CI (прогоняя все по очереди). Соответственно, для сброса состояния сервисов canopy должен командовать docker-compose - поднимать и завершать контейнеры. А если хорошо подумать, то можно параллельно поднимать несколько сетей из сервисов и исполнять несколько сценариев параллельно.

TODO: 

- дожидаться завершения команд docker-compose (требует более глубокого понимания F#)
- затем принимать в расчёт exit code
- а затем дожидаться готовности сервисов (реализовать через fetch + retry + exponential backoff)

В идеале скрипт выполнения пользовательских действий может переиспользоваться для локальной разработки 
(чтобы не таскать за собой устаревающие примеры мок-данных)

И, разумеется, стоит переиспользовать compose