--
-- PostgreSQL database dump
--

-- Dumped from database version 10.6
-- Dumped by pg_dump version 10.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: cambioclave(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.cambioclave() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
if(new."Clave" <> old."Clave") then
insert into public."TH_clave"("ID_usuario","Clave_ant","Fecha") values(OLD."ID_usuario",old."Clave",NOW());
end if;
return new;
end;
$$;


ALTER FUNCTION public.cambioclave() OWNER TO postgres;

--
-- Name: insertar_acceso(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insertar_acceso() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
if(new."Fecha_acceso" <> old."Fecha_acceso") then
insert into public."TH_acceso"("ID_usuario","Fecha_acceso") values(OLD."ID_usuario",NOW());
end if;
return new;
end;
$$;


ALTER FUNCTION public.insertar_acceso() OWNER TO postgres;

--
-- Name: promedio(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promedio() RETURNS TABLE(idmaestro integer, promedio bigint)
    LANGUAGE plpgsql
    AS $$
begin
return query select "ID_maestro", sum("Calificacion")/(select count(*) from public."T_CALIFICACIONES") 
from public."T_CALIFICACIONES" group by "ID_maestro";
end;
$$;


ALTER FUNCTION public.promedio() OWNER TO postgres;

--
-- Name: totalalumnos(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.totalalumnos() RETURNS TABLE(id_maestro integer, conteo bigint)
    LANGUAGE plpgsql
    AS $$
begin
return query select "ID_maestro", count("ID_maestro") from public."T_CALIFICACIONES" group by ("ID_maestro");
end;
$$;


ALTER FUNCTION public.totalalumnos() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: TH_acceso; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TH_acceso" (
    "I_seq" integer NOT NULL,
    "ID_usuario" integer NOT NULL,
    "Fecha_acceso" timestamp without time zone NOT NULL
);


ALTER TABLE public."TH_acceso" OWNER TO postgres;

--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TH_acceso_I_seq_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TH_acceso_I_seq_seq" OWNER TO postgres;

--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."TH_acceso_I_seq_seq" OWNED BY public."TH_acceso"."I_seq";


--
-- Name: TH_clave; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TH_clave" (
    "I_seq" integer NOT NULL,
    "ID_usuario" integer NOT NULL,
    "Clave_ant" character varying NOT NULL,
    "Fecha" timestamp without time zone NOT NULL
);


ALTER TABLE public."TH_clave" OWNER TO postgres;

--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TH_clave_I_seq_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TH_clave_I_seq_seq" OWNER TO postgres;

--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."TH_clave_I_seq_seq" OWNED BY public."TH_clave"."I_seq";


--
-- Name: T_CALIFICACIONES; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."T_CALIFICACIONES" (
    "ID_calificaciones" integer NOT NULL,
    "ID_maestro" integer NOT NULL,
    "ID_alumno" integer NOT NULL,
    "Calificacion" integer NOT NULL,
    "B_Final" boolean NOT NULL,
    "Fecha_Final" timestamp without time zone
);


ALTER TABLE public."T_CALIFICACIONES" OWNER TO postgres;

--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."T_CALIFICACIONES_ID_calificaciones_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."T_CALIFICACIONES_ID_calificaciones_seq" OWNER TO postgres;

--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."T_CALIFICACIONES_ID_calificaciones_seq" OWNED BY public."T_CALIFICACIONES"."ID_calificaciones";


--
-- Name: T_rol; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."T_rol" (
    "ID_rol" integer NOT NULL,
    "Descripcion" character varying NOT NULL
);


ALTER TABLE public."T_rol" OWNER TO postgres;

--
-- Name: T_usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."T_usuarios" (
    "Nombre" character varying NOT NULL,
    "Apellido" character varying NOT NULL,
    "Clave" character varying NOT NULL,
    "Sexo" "char" NOT NULL,
    "Rol" integer NOT NULL,
    "Fecha_registro" timestamp without time zone NOT NULL,
    "Fecha_acceso" timestamp without time zone,
    "B_activo" boolean NOT NULL,
    "ID_usuario" integer NOT NULL
);


ALTER TABLE public."T_usuarios" OWNER TO postgres;

--
-- Name: TH_acceso I_seq; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_acceso" ALTER COLUMN "I_seq" SET DEFAULT nextval('public."TH_acceso_I_seq_seq"'::regclass);


--
-- Name: TH_clave I_seq; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_clave" ALTER COLUMN "I_seq" SET DEFAULT nextval('public."TH_clave_I_seq_seq"'::regclass);


--
-- Name: T_CALIFICACIONES ID_calificaciones; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES" ALTER COLUMN "ID_calificaciones" SET DEFAULT nextval('public."T_CALIFICACIONES_ID_calificaciones_seq"'::regclass);


--
-- Data for Name: TH_acceso; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TH_acceso" ("I_seq", "ID_usuario", "Fecha_acceso") FROM stdin;
1	193440920	2018-12-11 16:25:20.167892
2	193440921	2018-12-11 17:19:16.576234
3	193440921	2018-12-11 17:20:21.918246
4	193440921	2018-12-11 17:25:15.895567
\.


--
-- Data for Name: TH_clave; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."TH_clave" ("I_seq", "ID_usuario", "Clave_ant", "Fecha") FROM stdin;
1	193440921	1234	2018-12-11 17:26:38.636611
\.


--
-- Data for Name: T_CALIFICACIONES; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."T_CALIFICACIONES" ("ID_calificaciones", "ID_maestro", "ID_alumno", "Calificacion", "B_Final", "Fecha_Final") FROM stdin;
2	193440920	593440121	100	f	\N
3	193440920	593440122	50	f	\N
4	193440920	593440123	100	f	\N
\.


--
-- Data for Name: T_rol; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."T_rol" ("ID_rol", "Descripcion") FROM stdin;
2	MAESTRO
3	ALUMNO
1	Administrador
\.


--
-- Data for Name: T_usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."T_usuarios" ("Nombre", "Apellido", "Clave", "Sexo", "Rol", "Fecha_registro", "Fecha_acceso", "B_activo", "ID_usuario") FROM stdin;
Alfredo	Lopez	1234	M	2	2018-12-11 15:29:44.59608	\N	t	193440920
Martha	Romero	1234	F	3	2018-12-11 15:31:27.281884	\N	t	593440121
Juan	Infante	1234	M	3	2018-12-11 15:32:12.900692	\N	t	593440122
María	Zamora	1234	F	3	2018-12-11 15:32:51.715023	\N	t	593440123
Diana	Reyes	1234	F	3	2018-12-11 15:46:20.860443	\N	t	593440124
Cecilia	Arana	1234	F	2	2018-12-11 15:48:33.911918	\N	t	193440930
Luis	Pasteur	12345	M	2	2018-12-11 15:47:13.969325	2018-12-11 17:25:15.895567	t	193440921
\.


--
-- Name: TH_acceso_I_seq_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TH_acceso_I_seq_seq"', 4, true);


--
-- Name: TH_clave_I_seq_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TH_clave_I_seq_seq"', 1, true);


--
-- Name: T_CALIFICACIONES_ID_calificaciones_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."T_CALIFICACIONES_ID_calificaciones_seq"', 4, true);


--
-- Name: TH_acceso TH_acceso_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_acceso"
    ADD CONSTRAINT "TH_acceso_pkey" PRIMARY KEY ("I_seq");


--
-- Name: TH_clave TH_clave_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TH_clave"
    ADD CONSTRAINT "TH_clave_pkey" PRIMARY KEY ("I_seq");


--
-- Name: T_CALIFICACIONES T_CALIFICACIONES_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES"
    ADD CONSTRAINT "T_CALIFICACIONES_pkey" PRIMARY KEY ("ID_calificaciones");


--
-- Name: T_rol T_rol_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_rol"
    ADD CONSTRAINT "T_rol_pkey" PRIMARY KEY ("ID_rol");


--
-- Name: T_usuarios T_usuarios_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_usuarios"
    ADD CONSTRAINT "T_usuarios_pkey" PRIMARY KEY ("ID_usuario");


--
-- Name: fki_T_calificaciones_alumno; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_T_calificaciones_alumno" ON public."T_CALIFICACIONES" USING btree ("ID_alumno");


--
-- Name: fki_T_calificaciones_maestro; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_T_calificaciones_maestro" ON public."T_CALIFICACIONES" USING btree ("ID_maestro");


--
-- Name: fki_T_usuarios_rol; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_T_usuarios_rol" ON public."T_usuarios" USING btree ("Rol");


--
-- Name: T_usuarios TR_I_claves; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER "TR_I_claves" BEFORE UPDATE ON public."T_usuarios" FOR EACH ROW EXECUTE PROCEDURE public.cambioclave();


--
-- Name: T_usuarios tr_i_acceso; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER tr_i_acceso BEFORE UPDATE ON public."T_usuarios" FOR EACH ROW EXECUTE PROCEDURE public.insertar_acceso();


--
-- Name: T_CALIFICACIONES T_calificaciones_alumno; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES"
    ADD CONSTRAINT "T_calificaciones_alumno" FOREIGN KEY ("ID_alumno") REFERENCES public."T_usuarios"("ID_usuario");


--
-- Name: T_CALIFICACIONES T_calificaciones_maestro; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_CALIFICACIONES"
    ADD CONSTRAINT "T_calificaciones_maestro" FOREIGN KEY ("ID_maestro") REFERENCES public."T_usuarios"("ID_usuario");


--
-- Name: T_usuarios Usuario_rol; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."T_usuarios"
    ADD CONSTRAINT "Usuario_rol" FOREIGN KEY ("Rol") REFERENCES public."T_rol"("ID_rol");


--
-- PostgreSQL database dump complete
--

