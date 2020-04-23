open canopy.runner.classic
open canopy.configuration
open canopy.classic
open canopy.types

chromeDir <- System.AppContext.BaseDirectory
let browser = start ChromeHeadless

context "Simple counter test"

before (fun _ ->
    printf("Starting services...\n")
    System.Diagnostics.Process.Start("docker-compose", "up -d") |> ignore
    sleep(3)
)

after (fun _ ->
    printf("Stopping services...\n")
    System.Diagnostics.Process.Start("docker-compose", "down -v") |> ignore
    sleep(3)
)

(* 
    TODO: 
    - нужно дожидаться завершения команд docker-compose (требует более глубокого понимания F#)
    - затем принимать в расчёт exit code
    - а затем дожидаться готовности сервисов (реализовать через fetch + retry + exponential backoff)

    В идеале скрипт выполнения пользовательских действий может переиспользоваться для локальной разработки 
    (чтобы не таскать за собой устаревающие примеры мок-данных)

    И, разумеется, стоит переиспользовать compose
*)

let indicator = css ".elmish-app>div>div"

"Increment" &&& fun _ ->
    sleep(3)
    url "http://localhost:8080/"
    indicator == "0"
    click (text "+")
    indicator == "1"
    click (text "+")
    indicator == "2"

"Decrement" &&& fun _ ->
    url "http://localhost:8080/"
    indicator == "0"
    click (text "-")
    indicator == "-1"
    click (text "-")
    indicator == "-2"

run()

quit()