package com.fraudplatform.fraud_service.fraud;

import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;

@Service
public class TransactionConsumer {

    @KafkaListener(topics = "transactions", groupId = "fraud-service")
    public void consume(String message) {
        System.out.println("Evento recibido: " + message);
    }
}