pipeline {
    agent any

    stages {
        stage('Download source') {
            steps {
				script {
					res = sh(script: "test -d /home/source && echo '1' || echo '0' ", returnStdout: true).trim()
					if(res=='1'){
						sh 'rm -R /home/source'
					}				
				}
			
                sh 'mkdir /home/source'
				sh 'git clone https://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@github.com/mistek131995/VPNBot.git /home/source'
            }
        }
        stage('Create image') {
            steps {
                sh 'docker build -t vpn-api-${BUILD_NUMBER} /home/source'
            }
        }
		stage('Stop container') {
            steps {
                script{
                    def lastSuccessfulBuildID = 0
                    def build = currentBuild.previousBuild
                    while (build != null) {
                        if (build.result == "SUCCESS")
                        {
                            lastSuccessfulBuildID = build.id as Integer
                            break
                        }
                        build = build.previousBuild
                    }
                    println lastSuccessfulBuildID
                }
            }
        }
		stage('Start container') {
            steps {
                sh 'docker run -d vpn-api-${BUILD_NUMBER}'
            }
        }
        stage('Delete source') {
            steps {
                sh 'rm -R /home/source'
            }
        }
    }
}