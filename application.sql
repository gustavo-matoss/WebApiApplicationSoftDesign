create table Application(
  APPLICATION int not null default '1',
  url text,
  pathLocal text,
  debuggingMode int not null default '0'
  CONSTRAINT pkApplication PRIMARY KEY (DATA,CODIGO_EMPRESA,CODIGO_LOJA,CODIGO_REDE,NSU)
);