package com.turen.llk.server.domain;
import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;
@PersistenceCapable(identityType = IdentityType.APPLICATION)
public class ChengJi {
	@PrimaryKey
    @Persistent(valueStrategy = IdGeneratorStrategy.IDENTITY)
    private Long id;

    @Persistent
    private String username;
    @Persistent
    private int seconds;
    
    public int getSeconds() {
		return seconds;
	}

	public void setSeconds(int seconds) {
		this.seconds = seconds;
	}

	public void setId(Long id) {
            this.id = id;
    }

    public Long getId() {
            return id;
    }

    public void setUsername(String username) {
            this.username = username;
    }

    public String getUsername() {
            return username;
    }
}
/*import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

@PersistenceCapable(identityType = IdentityType.APPLICATION)
public class User {
        @PrimaryKey
        @Persistent(valueStrategy = IdGeneratorStrategy.IDENTITY)
        private Long id;

        @Persistent
        private String username;
        @Persistent
        private String password;
        public void setId(Long id) {
                this.id = id;
        }

        public Long getId() {
                return id;
        }

        public void setPassword(String password) {
                this.password = password;
        }

        public String getPassword() {
                return password;
        }

        public void setUsername(String username) {
                this.username = username;
        }

        public String getUsername() {
                return username;
        }
        
}
*/
