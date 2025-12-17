import React from "react";
import './style.scss';

interface ModalProps {
  open: boolean;
  title?: string;
  message?: string;
  variant?: "success" | "error" | "info";
  onClose: () => void;
}

export default function Modal({ open, title, message, variant = "info", onClose }: ModalProps) {
  if (!open) return null;

  const headerStyle: React.CSSProperties = {
    backgroundColor:
      variant === "success"
        ? "hsl(var(--success))"
        : variant === "error"
        ? "hsl(var(--destructive))"
        : "hsl(var(--brand-yellow))",
    color: "hsl(var(--brand-dark))",
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-end posicionamentoModal">
      <div className="absolute inset-0 bg-black/40" onClick={onClose} />
      <div className="relative w-full max-w-md rounded-lg overflow-hidden shadow-lg animate-scale-in">
        <div className="px-4 py-3" style={headerStyle}>
          <div className="font-semibold text-sm">{title ?? (variant === "success" ? "Success" : variant === "error" ? "Error" : "Info")}</div>
        </div>
        <div className="bg-card px-4 py-4">
          <div className="text-sm text-muted-foreground">{message}</div>
          <div className="mt-4 flex justify-end">
            <button
              className="rounded-md px-3 py-1 text-sm font-medium"
              style={{ backgroundColor: "hsl(var(--brand-yellow))", color: "hsl(var(--brand-dark))" }}
              onClick={onClose}
            >
              OK
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
