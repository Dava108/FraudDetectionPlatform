package com.fraudplatform.fraud_service.service;

import java.util.UUID;

public class FraudAnalysisResult {

    private UUID transactionId;
    private boolean fraudulent;
    private String reason;

    public FraudAnalysisResult(
            UUID transactionId,
            boolean fraudulent,
            String reason) {
        this.transactionId = transactionId;
        this.fraudulent = fraudulent;
        this.reason = reason;
    }

    public UUID getTransactionId() {
        return transactionId;
    }

    public boolean isFraudulent() {
        return fraudulent;
    }

    public String getReason() {
        return reason;
    }
}
