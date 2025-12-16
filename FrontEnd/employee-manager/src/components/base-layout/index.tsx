
interface IBaseLayoutProps {
  children: React.JSX.Element;
}

export default function BaseLayout(props:IBaseLayoutProps): React.JSX.Element {
  return(
     <div className="base-layout">
      <main className="base-layout__content">
        <div className="base-layout__inner">
          {props.children}
        </div>
      </main>
    </div>
  );
}