interface PageHeaderProps {
  title: string;
  description?: string;
}

export default function Header({ title, description }: PageHeaderProps) {
  return (
    <div className="mb-8 animate-fade-in">
      <h1 className="text-3xl font-bold text-foreground text-left">{title}</h1>
      {description && (
        <p className="mt-2 text-muted-foreground text-left">{description}</p>
      )}
    </div>
  );
}