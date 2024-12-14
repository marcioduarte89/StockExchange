# First stage: Full-fledged Alpine image
FROM alpine as builder
RUN apk update && apk add busybox-extras

# Second stage: Prometheus image
FROM prom/prometheus
# Copy ping binary from the first stage
COPY --from=builder /bin/ping /bin/ping