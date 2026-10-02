package com.fraudplatform.fraud_service.events;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.math.BigDecimal;
import java.time.Instant;
import java.util.UUID;

public class TransactionCreatedEvent {

    @JsonProperty("TransactionId")
    private UUID transactionId;

    @JsonProperty("Amount")
    private BigDecimal amount;

    @JsonProperty("Currency")
    private String currency;

    @JsonProperty("CardId")
    private String cardId;

    @JsonProperty("MerchantId")
    private String merchantId;

    @JsonProperty("Timestamp")
    private Instant timestamp;

    public UUID getTransactionId() {
        return transactionId;
    }

    public void setTransactionId(UUID transactionId) {
        this.transactionId = transactionId;
    }

    public BigDecimal getAmount() {
        return amount;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public String getCurrency() {
        return currency;
    }

    public void setCurrency(String currency) {
        this.currency = currency;
    }

    public String getCardId() {
        return cardId;
    }

    public void setCardId(String cardId) {
        this.cardId = cardId;
    }

    public String getMerchantId() {
        return merchantId;
    }

    public void setMerchantId(String merchantId) {
        this.merchantId = merchantId;
    }

    public Instant getTimestamp() {
        return timestamp;
    }

    public void setTimestamp(Instant timestamp) {
        this.timestamp = timestamp;
    }
}