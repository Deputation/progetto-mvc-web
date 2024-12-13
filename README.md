# Progetto MVC Web - Punti di Pagamento Milano

Applicazione web sviluppata in ASP.NET Core MVC per la visualizzazione 
e la ricerca dei punti di pagamento dei bolli e della tassa auto a Milano.

## Funzionalità Implementate

### Mappa Interattiva
- Visualizzazione dei punti di pagamento su mappa usando Leaflet
- Selezione dei punti con visualizzazione dettagli
- Calcolo distanze tra la posizione utente (selezionabile con doppio click) e i punti di pagamento

### Servizi Backend
- **Client REST**
  - Interfacciamento con il servizio dati di Milano
  - Implementazione con supporto cache per ottimizzare le performance e ridurre le chiamate al servizio esterno
  - Gestione automatica del refresh dei dati
  - Gestione robusta degli errori con logging

- **SOAP Service**
  - Endpoint per recupero singolo punto di pagamento
  - Endpoint per recupero lista completa punti
  - Integrazione con il client REST
  - Gestione degli errori con FaultException

- **API di Ricerca**
  - Endpoint per trovare i punti di pagamento più vicini data una posizione
  - Calcolo delle distanze tra coordinate geografiche con la formula dell'emisenoverso
  - Ordinamento dei risultati per distanza

### Documentazione API
- Swagger UI per le API REST
- WSDL disponibile per i servizi SOAP
- Interfaccia web con accesso diretto alla documentazione

### Containerizzazione
- Configurazione tramite docker-compose
- Ambiente isolato e facilmente distribuibile

## Avvio del Progetto

### Utilizzo con Docker

1. Clonare il repository:
2. Comporre e avviare i container:
```bash
docker-compose up
```

3. Accedere all'applicazione:
- Web UI: http://localhost:8080
- Swagger: Accessibile tramite bottone sulla home
- SOAP WSDL: Accessibile tramite bottone sulla home

## Test dei Servizi

### REST API
Accedere a Swagger UI all'indirizzo `/swagger` o tramite la home per:
- Visualizzare la documentazione completa delle API non SOAP
- Testare le chiamate direttamente dall'interfaccia
- Vedere esempi di request/response

### SOAP Service
Il servizio SOAP può essere testato in diversi modi:

1. Tramite il WSDL disponibile all'indirizzo `/PaymentPointService.svc?wsdl` o tramite la home
2. Usando tool come SoapUI o Postman

Le operazioni disponibili sono:
- `GetAllPaymentPoints`: Recupero di tutti i punti di pagamento
- `GetPaymentPointById`: Recupero di un singolo punto tramite ID

## Note Aggiuntive

- Le configurazioni sono già presenti nei file `appsettings.json` e `appsettings.Development.json`
- Il sistema di cache è configurato per ottimizzare le performance e ridurre il carico sul servizio esterno
- Il logging è configurato per tracciare errori e comportamenti anomali
- L'interfaccia web è completamente localizzata in italiano