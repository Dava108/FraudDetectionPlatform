package com.fraudplatform.fraud_service.fraud;

import com.fraudplatform.fraud_service.events.TransactionCreatedEvent;
import com.fraudplatform.fraud_service.service.FraudDetectionService;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;
import tools.jackson.databind.json.JsonMapper;
import com.fraudplatform.fraud_service.service.FraudAnalysisResult;

@Service
public class TransactionConsumer {

    private final JsonMapper jsonMapper;
    private final FraudDetectionService fraudDetectionService;

    public TransactionConsumer(
            JsonMapper jsonMapper,
            FraudDetectionService fraudDetectionService) {
        this.jsonMapper = jsonMapper;
        this.fraudDetectionService = fraudDetectionService;
    }

    @KafkaListener(topics = "transactions", groupId = "fraud-service")
    public void consume(String message) {
        try {
            TransactionCreatedEvent transaction = jsonMapper.readValue(message, TransactionCreatedEvent.class);

            FraudAnalysisResult result = fraudDetectionService.analyze(transaction);

            System.out.println(
                    "Transacción: " + result.getTransactionId()
                            + " | Fraude: " + result.isFraudulent()
                            + " | Motivo: " + result.getReason());

        } catch (Exception e) {
            System.err.println("Error procesando transacción: " + e.getMessage());
        }
    }
}