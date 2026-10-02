package com.fraudplatform.fraud_service.service;

import com.fraudplatform.fraud_service.events.TransactionCreatedEvent;
import org.springframework.stereotype.Service;

import java.time.Instant;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

@Service
public class FraudDetectionService {

    private static final double MAX_ALLOWED_AMOUNT = 10000.0;
    private static final long RAPID_TRANSACTION_WINDOW_SECONDS = 60;

    private final Map<String, Instant> lastTransactionByCard = new ConcurrentHashMap<>();

    public FraudAnalysisResult analyze(TransactionCreatedEvent transaction) {

        if (transaction.getAmount().doubleValue() > MAX_ALLOWED_AMOUNT) {
            return new FraudAnalysisResult(
                    transaction.getTransactionId(),
                    true,
                    "Amount exceeds maximum allowed limit"
            );
        }

        Instant now = Instant.now();

        Instant lastTransaction =
                lastTransactionByCard.put(transaction.getCardId(), now);

        if (lastTransaction != null) {

            long secondsSinceLastTransaction =
                    now.getEpochSecond() - lastTransaction.getEpochSecond();

            if (secondsSinceLastTransaction <= RAPID_TRANSACTION_WINDOW_SECONDS) {
                return new FraudAnalysisResult(
                        transaction.getTransactionId(),
                        true,
                        "Multiple transactions detected for the same card within 60 seconds"
                );
            }
        }

        return new FraudAnalysisResult(
                transaction.getTransactionId(),
                false,
                "Transaction passed fraud rules"
        );
    }
}